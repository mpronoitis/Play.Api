namespace Play.Infra.Data.Repository;

public class UserRepository : IUserRepository
{
    protected readonly PlayCoreContext Db;
    protected readonly DbSet<User> DbSet;

    public UserRepository(PlayCoreContext context)
    {
        Db = context;
        DbSet = Db.Set<User>();
    }

    //we will use unitOfWork pattern to make sure that controllers make use of same context
    public IUnitOfWork UnitOfWork => Db;

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    //get all async with pagination
    public async Task<IEnumerable<User>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var users = await DbSet.AsNoTracking().OrderBy(c => c.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        //strip PasswordHash and PasswordSalt
        users.ForEach(u => u.PasswordHash = u.Salt = null);

        return users;
    }

    //get by email async
    public async Task<User> GetByEmailAsync(string email)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
    }

    //hash password
    public (string, string) HashPassword(string password)
    {
        return Crypto.Crypt(password);
    }

    //check if password is valid
    public bool CheckPassword(string password, string hash, string salt)
    {
        return Crypto.Verify(password, hash, salt);
    }

    //verify that a user exists based on email
    public async Task<bool> ExistsAsync(string email)
    {
        return await DbSet.AsNoTracking().AnyAsync(c => c.Email == email);
    }

    //verify if a user (by email) has OtpSecret
    public async Task<bool> HasOtpSecretAsync(string email)
    {
        return await DbSet.AsNoTracking().AnyAsync(c => c.Email == email && !string.IsNullOrEmpty(c.OtpSecret));
    }


    //add
    public void Add(User user)
    {
        DbSet.Add(user);
    }

    //update
    public void Update(User user)
    {
        DbSet.Update(user);
    }

    //remove
    public void Remove(User user)
    {
        DbSet.Remove(user);
    }

    //get total count
    public async Task<int> GetTotalCount()
    {
        return await DbSet.CountAsync();
    }

    /// <summary>
    ///     Get total count of users with a given role
    ///     Optionally pass a time range to get count of users with a given role with CreatedAt in that range
    /// </summary>
    /// <param name="role"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<int> GetTotalCountByRoleAsync(string role, DateTime? from = null, DateTime? to = null)
    {
        var query = DbSet.AsNoTracking().Where(u => u.Role == role);

        if (from != null && to != null)
            query = query.Where(u => u.CreatedAt >= from && u.CreatedAt <= to);

        return await query.CountAsync();
    }

    /// <summary>
    ///     Get count of users created within a given time range
    /// </summary>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    public async Task<int> GetTotalCountByTimeRangeAsync(DateTime startDateTime, DateTime endDateTime)
    {
        return await DbSet.AsNoTracking().Where(u => u.CreatedAt >= startDateTime && u.CreatedAt <= endDateTime)
            .CountAsync();
    }


    public void Dispose()
    {
        Db.Dispose();
    }

    //flush tracked changes
    public void Flush()
    {
        //remove all tracked changes
        Db.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);
    }
}

/// <summary>
///     Implementation of PBKDF2 hashing algorithm for password storage.
///     Proceed with caution you might end up locking yourself out of your own account.
///     (ノಠ益ಠ)ノ彡┻━┻
/// </summary>
public static class Crypto
{
    private static readonly uint[] Lookup32 = CreateLookup32();

    private static readonly Random _random = new();

    /// <summary>
    ///     Crypt Function , receives a password and returns a hashed password and salt.
    /// </summary>
    /// <param name="password">Password to be hashed.</param>
    /// <returns>Hashed password and salt.</returns>
    public static (string, string) Crypt(string password)
    {
        //generate random salt string up to 32 characters long
        var salt = GenerateSalt(32);
        //hash password
        var hash = Pbkdf2Hash(password, salt);
        return (hash, salt);
    }

    /// <summary>
    ///     Verify Function , receives a password and a hashed password and salt and returns true if the password is valid.
    /// </summary>
    /// <param name="password">Password to be verified.</param>
    /// <param name="hash">Hashed password.</param>
    /// <param name="salt">Salt.</param>
    /// <returns>True if password is valid.</returns>
    public static bool Verify(string password, string hash, string salt)
    {
        //if password is empty return false
        if (string.IsNullOrEmpty(password))
            return false;
        //hash password
        var newHash = Pbkdf2Hash(password, salt);
        //compare hashes
        return newHash == hash;
    }

    private static string GenerateSalt(int i)
    {
        //generate a random string of i length
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var builder = new StringBuilder(i);
        for (var j = 0; j < i; j++) builder.Append(chars[_random.Next(chars.Length)]);
        return builder.ToString();
    }


    /// <summary>
    ///     Hashes a password with PBKDF2.
    /// </summary>
    /// <param name="input">The password to hash.</param>
    /// <param name="salt">The salt string to use in the hash.</param>
    /// <returns>The hashed password in string.</returns>
    private static string Pbkdf2Hash(string input, string salt)
    {
        //convert salt string to byte[]
        var saltBytes = HexToByteArrayViaLookup32(salt);
        // Generate the hash
        var pbkdf2 = new Rfc2898DeriveBytes(input, saltBytes, 10000, HashAlgorithmName.SHA512);
        var hashBytes = pbkdf2.GetBytes(256); //256 bytes length
        return ByteArrayToHexViaLookup32(hashBytes);
    }

    /// <summary>
    ///     Helper function to create lookup table.
    /// </summary>
    /// <returns>Lookup32 uint[]</returns>
    private static uint[] CreateLookup32()
    {
        var result = new uint[256];
        for (var i = 0; i < 256; i++)
        {
            var s = i.ToString("X2");
            result[i] = s[0] + ((uint)s[1] << 16);
        }

        return result;
    }

    /// <summary>
    ///     Function to convert byte array to hex string.
    /// </summary>
    /// <param name="bytes">The bytes array to convert</param>
    /// <returns></returns>
    private static string ByteArrayToHexViaLookup32(byte[] bytes)
    {
        var lookup32 = Lookup32;
        var result = new char[bytes.Length * 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            var val = lookup32[bytes[i]];
            result[2 * i] = (char)val;
            result[2 * i + 1] = (char)(val >> 16);
        }

        return new string(result);
    }

    /// <summary>
    ///     Function to convert hex string to byte array.
    /// </summary>
    /// <param name="hex">The hex string to convert</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if hex is not a valid hex string.</exception>
    /// <exception cref="ArgumentNullException">Thrown if hex is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if hex is not a valid hex string.</exception>
    private static byte[] HexToByteArrayViaLookup32(string hex)
    {
        if (hex == null)
            throw new ArgumentNullException(nameof(hex));
        if (hex.Length % 2 != 0)
            throw new ArgumentException("Hex string must have even length.");
        var lookup32 = Lookup32;
        var result = new byte[hex.Length / 2];
        for (var i = 0; i < hex.Length; i += 2)
        {
            var val = lookup32[hex[i]] + lookup32[hex[i + 1]] * 16;
            result[i / 2] = (byte)val;
        }

        return result;
    }
}
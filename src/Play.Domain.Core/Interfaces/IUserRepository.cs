using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Interfaces;

public interface IUserRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<User> GetByEmailAsync(string email);
    (string, string) HashPassword(string password);
    bool CheckPassword(string password, string hash, string salt);
    void Add(User user);
    void Update(User user);

    void Remove(User user);

    //get total count
    Task<int> GetTotalCount();

    //verify that a user exists based on email
    Task<bool> ExistsAsync(string email);

    //verify if a user (by email) has OtpSecret
    Task<bool> HasOtpSecretAsync(string email);

    /// <summary>
    ///     Get count of users created within a given time range
    /// </summary>
    /// <param name="startDateTime"></param>
    /// <param name="endDateTime"></param>
    /// <returns></returns>
    Task<int> GetTotalCountByTimeRangeAsync(DateTime startDateTime, DateTime endDateTime);

    /// <summary>
    ///     Get total count of users with a given role
    ///     Optionally pass a time range to get count of users with a given role with CreatedAt in that range
    /// </summary>
    /// <param name="role"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<int> GetTotalCountByRoleAsync(string role, DateTime? from = null, DateTime? to = null);

    void Dispose();

    //flush tracked changes
    void Flush();
}
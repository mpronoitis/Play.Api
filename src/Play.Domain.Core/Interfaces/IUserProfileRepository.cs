using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Interfaces;

public interface IUserProfileRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<UserProfile> GetByIdAsync(Guid id);

    Task<UserProfile> GetByUserId(Guid user_id);


    void Update(UserProfile newUserProfile);

    Task<IEnumerable<UserProfile>> GetAllAsync(int page = 1, int pageSize = 10);

    void Add(UserProfile newUserProfile);

    void Remove(UserProfile newUserProfile);
}
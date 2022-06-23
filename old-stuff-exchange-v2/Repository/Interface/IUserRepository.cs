using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IUserRepository<T>
    {
        Task<UserResponseModel> Create(UserResponseModel user);
        string Login(string email);
        Task<UserResponseModel> Login(string userName, string password);
        Task<UserResponseModel> GetById(Guid id);
        Task<UserResponseModel> GetByEmail(string email);
        Task<List<UserResponseModel>> GetList(string email,Guid? roleId, int pageNumber, int pageSize);
        Task<bool> Delete(Guid id);
        Task<bool> Update(UserResponseModel newUser);
        Task<UserResponseModel> UpdateUserAddress(Guid UserId, Guid BuildingId);
    }
}

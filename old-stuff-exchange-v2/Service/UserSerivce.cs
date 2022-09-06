using FirebaseAdmin.Auth;
using Old_stuff_exchange.Model.User;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Entities.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Service
{
    public class UserService
    {
        private readonly IUserRepository<User> _repo;
        public UserService(IUserRepository<User> repo)
        {
            _repo = repo;
        }
        public async Task<User> Create(User user)
        {
            return await _repo.Create(user);
        }
        public string Login(string email, UserRecord user)
        {
            return _repo.Login(email, user);
        }
        public async Task<User> GetById(Guid id) {
            return await _repo.GetById(id);
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _repo.GetByEmail(email);
        }
        public async Task<List<ResponseUserModel>> GetList(string email,Guid? roleId,Guid? apartmentId, int pageNumber, int pageSize)
        {
            List<User> users = await _repo.GetList(email, roleId,apartmentId, pageNumber, pageSize);
            List<ResponseUserModel> result = users.ConvertAll<ResponseUserModel>(user => user.ToResponseModel());
            return result;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repo.Delete(id);
        }
        public async Task<bool> Update(User newUser)
        {
            return await _repo.Update(newUser);
        }

        public async Task<User> UpdateUserAddress(Guid UserId, Guid BuildingId) { 
            return await _repo.UpdateUserAddress(UserId, BuildingId);
        }

        public async Task<User> Login(string userName, string password) { 
            return await _repo.Login(userName, password);
        }
    }
}

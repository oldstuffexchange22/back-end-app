﻿using Old_stuff_exchange.Model.User;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
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
        public string Login(string email)
        {
            return _repo.Login(email);
        }
        public User GetByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        public async Task<List<ResponseUserModel>> GetList(string email,Guid? roleId, int pageNumber, int pageSize)
        {
            List<User> users = await _repo.GetList(email, roleId, pageNumber, pageSize);
            List<ResponseUserModel> result = users.ConvertAll<ResponseUserModel>(user => new ResponseUserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Status = user.Status,
                Phone = user.Phone,
                Email = user.Email,
                Image = user.ImagesUrl,
                CreatedAt = user.CreatedAt,
                RoleName = user?.Role.Name,
                BuildingName = user?.Building.Name
            });
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
    }
}

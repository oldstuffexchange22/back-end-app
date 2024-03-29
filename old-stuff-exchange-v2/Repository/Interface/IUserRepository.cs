﻿using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IUserRepository<T>
    {
        Task<User> Create(User user);
        string Login(string email);
        User GetByEmail(string email);
        Task<List<User>> GetList(string email,Guid? roleId, int pageNumber, int pageSize);
        Task<bool> Delete(Guid id);
        Task<bool> Update(User newUser);
    }
}

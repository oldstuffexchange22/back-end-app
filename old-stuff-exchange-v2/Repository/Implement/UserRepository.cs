﻿using Microsoft.EntityFrameworkCore;
using Old_stuff_exchange.Helper;
using Old_stuff_exchange.Model;
using Old_stuff_exchange.Repository.Interface;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Role;
using old_stuff_exchange_v2.Enum.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Implement
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly AppDbContext _context;
        private readonly IJwtHelper _jwtHelper;
        public UserRepository(AppDbContext context, IJwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }
        public async Task<User> Create(User user)
        {
            User tempUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (tempUser == null)
            {
                Role role = _context.Roles.FirstOrDefault(r => r.Name == RoleNames.ADMIN);
                if (role != null) user.Role = role;
                await _context.Users.AddAsync(user); 
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }
        public async Task<User> GetByEmail(string email)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return await Task.FromResult(user);
        }

        public string Login(string email)
        {
            if(string.IsNullOrEmpty(email)) return null;
            User user = _context.Users.Include(user => user.Role).Include(user => user.Building)
                .FirstOrDefault(u => u.Email == email);
            if (user == null) {
                // if (user.Status == UserStatus.INACTIVE) return UserStatus.INACTIVE;
                Role residentRole = _context.Roles.FirstOrDefault(role => role.Name == RoleNames.RESIDENT);
                User newUser = new User
                {
                    UserName = email,
                    Email = email,
                    Status = UserStatus.INACTIVE,
                    Role = residentRole,
                    FullName = email,
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return _jwtHelper.generateJwtToken(newUser);
            }
            
            return _jwtHelper.generateJwtToken(user);
        }
        public async Task<bool> Delete(Guid id)
          {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            user.Status = UserStatus.INACTIVE;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(User newUser)
        {
            User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == newUser.Id);
            if (user == null)
            {
                return false;
            }
            newUser.UserName = user.UserName;
            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetList(string email,Guid? roleId, int pageNumber, int pageSize)
        {
            var allUser = _context.Users.Include(u => u.Role).Include(u => u.Building)
                          .AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(email)) allUser = allUser.Where(u => u.Email.ToUpper().Contains(email.ToUpper()));
            if (roleId != null) allUser = allUser.Where(u => u.RoleId == roleId);
            #endregion

            #region Paging
            var result = PaginatedList<User>.Create(allUser, pageNumber, pageSize);
            #endregion
            return await Task.FromResult(result.ToList());
        }

        public async Task<User> UpdateUserAddress(Guid UserId, Guid BuildingId)
        {
            Building building =await _context.Buildings.FindAsync(BuildingId);
            User user = await _context.Users.FindAsync(UserId);
            if (building == null || user == null) return null;
            user.Building = building;
            user.Status = UserStatus.ACTIVE;
            _context.Users.Update(user);
             await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            return await Task.FromResult(_context.Users.Find(id));
        }
    }
}

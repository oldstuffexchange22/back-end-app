﻿using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Old_stuff_exchange.Repository.Interface
{
    public interface IPostRepository<T>
    {
        Task<Post> Create(Post post);
        Task<Post> Update(Post post);
        Task<Post> AcceptPost(Guid id);
        Task<Post> NotAcceptPost(Guid id);
        Task<bool> Delete(Guid id);
        Task<Post> GetPostById(Guid id);
        Task<List<Post>> GetList(Guid? apartmentId, Guid? categoriesId, PagingModel model);
        Task<List<Post>> GetListByUserId(Guid userId, string status, int page, int pageSize);
    }
}

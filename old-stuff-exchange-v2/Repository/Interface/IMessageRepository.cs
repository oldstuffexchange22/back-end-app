using old_stuff_exchange_v2.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Interface
{
    public interface IMessageRepository 
    {
        Task<Message> Create(Message message);
        Task<List<Message>> GetList(Guid senderId,Guid receiverId, int page, int pageSize,bool isFull);
    }
}

using Old_stuff_exchange.Model;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Repository.Implement
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> Create(Message message)
        {
            await _context.Messages.AddAsync(message);
            int result = _context.SaveChanges();
            return result == 1 ? message : null;
        }

        public Task<List<Message>> GetList(Guid senderId, Guid receiverId, int page , int pageSize, bool isFull)
        {
            List<Guid> Participants = new List<Guid>();
            Participants.Add(senderId);
            Participants.Add(receiverId);
            var allMessage = _context.Messages.AsQueryable();
            allMessage = allMessage.Where(mess => (mess.SenderId == senderId || mess.SenderId == receiverId) && (mess.ReceiverId == senderId || mess.ReceiverId == receiverId));

            #region Sorting
            allMessage = allMessage.OrderBy(d => d.CreatedAt);
            #endregion
            #region Paging
            if (isFull)
            {
                var result = allMessage;
                return Task.FromResult(result.ToList());
            }
            else { 
                var result = PaginatedList<Message>.Create(allMessage, page, pageSize);
                return Task.FromResult(result.ToList());
            }
            #endregion
            
        }
    }
}

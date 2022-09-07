using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Model.Message;
using old_stuff_exchange_v2.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Service
{
    public class MessageService
    {
        private readonly IMessageRepository _repo;
        public MessageService(IMessageRepository repo)
        {
            _repo = repo;
        }

        public async Task<Message> Create(CreateMessageModel model)
        {
            Message message = new Message
            {
                Content = model.Content,
                SenderId = model.SenderId,
                ReceiverId = model.ReceiverId,
            };
            return await _repo.Create(message);
        }


        public async Task<List<Message>> GetList(Guid senderId, Guid receiverId, int page = 1, int pageSize = 10, bool isFull = false)
        {
            List<Message> response = await _repo.GetList(senderId, receiverId, page, pageSize, isFull);
            return response;
        }

    }
}

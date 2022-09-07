using System;

namespace old_stuff_exchange_v2.Model.Message
{
    public class CreateMessageModel
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }
}

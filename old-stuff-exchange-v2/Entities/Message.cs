using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace old_stuff_exchange_v2.Entities
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

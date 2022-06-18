using System;

namespace old_stuff_exchange_v2.Model.Post
{
    public class PostStatusModel
    {
        public Guid PostId { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public string WalletType { get; set; }
    }
}

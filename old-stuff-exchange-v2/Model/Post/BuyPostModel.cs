using System;

namespace old_stuff_exchange_v2.Model.Post
{
    public class BuyPostModel
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string WalletType { get; set; }
    }
}

using System.Collections.Generic;

namespace old_stuff_exchange_v2.Enum.Post
{
    public class PostStatus
    {
        public const string ACTIVE = "ACTIVE";
        public const string WAITING = "WAITING";
        public const string INACTIVE = "INACTIVE";
        public const string DELIVERY = "DELIVERY";
        public const string DELIVERED = "DILIVERED";
        public const string ACCOMPLISHED = "ACCOMPLISHED";
        public const string FAILURE = "FAILURE";

        public static List<string> ListPostSatus()
        {
            List<string> listSatus = new List<string>();
            listSatus.Add(ACTIVE);
            listSatus.Add(INACTIVE);
            listSatus.Add(WAITING);
            listSatus.Add(DELIVERY);
            listSatus.Add(DELIVERED);
            listSatus.Add(ACCOMPLISHED);
            listSatus.Add(FAILURE);
            return listSatus;
        }
    }
}

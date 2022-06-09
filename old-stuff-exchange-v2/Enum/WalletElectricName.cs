using System.Collections.Generic;

namespace old_stuff_exchange_v2.Enum
{
    public class WalletElectricName
    {
        public const string MOMO = "MOMO";
        public const string MOCA = "MOCA";
        public const string ZALOPAY = "ZALOPAY";
        public const string VIETTELPAY = "VIETTELPAY";

        public static List<string> GetWalletNames() { 
            List<string> names = new List<string>();
            names.Add(MOMO);
            names.Add(MOCA);
            names.Add(ZALOPAY);
            names.Add(VIETTELPAY);
            return names;
        }
    }
}

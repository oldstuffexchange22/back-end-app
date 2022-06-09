using System.Collections.Generic;

namespace old_stuff_exchange_v2.Enum
{
    public class TransactionType
    {
        public const string BOUGHT = "BOUGHT";
        public const string SELL = "SELL";
        public const string RECHARGE = "RECHARGE";

        public static List<string> GetTransactionPost() { 
            List<string> transactions = new List<string>();
            transactions.Add(BOUGHT);
            transactions.Add(SELL);
            return transactions;
        }

        public static List<string> GetTransactions()
        {
            List<string> transactions = new List<string>();
            transactions.Add(BOUGHT);
            transactions.Add(SELL);
            transactions.Add(RECHARGE);
            return transactions;
        }
    }
}

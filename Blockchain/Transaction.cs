using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    class Transaction
    {
        public readonly string Sender;
        public readonly string Recipient;
        public readonly int Amount;

        public Transaction(string sender, string recipient, int amount)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
        }
    }
}

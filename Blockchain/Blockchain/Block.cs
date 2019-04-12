using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    class Block
    {
        public readonly int Index;
        public readonly DateTime Timestamp;
        public readonly List<Transaction> CurrentTransactions;
        public readonly int Proof;
        public readonly string PreviousHash;

        public Block(int index, DateTime timestamp, IEnumerable<Transaction> transactions, int proof,
            string previousHash)
        {
            Index = index;
            Timestamp = timestamp;
            CurrentTransactions = new List<Transaction>(transactions);
            Proof = proof;
            PreviousHash = previousHash;
        }
    }
}

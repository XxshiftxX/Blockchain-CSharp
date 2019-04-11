using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blockchain
{
    class Blockchain
    {
        public List<Block> Chain = new List<Block>();
        public List<Transaction> CurrentTransactions = new List<Transaction>();

        public Block LastBlock { get => Chain.Last(); }

        public Blockchain()
        {
            CreateBlock(100, "1" );
        }

        public static string Hash(Block block)
        {
            var jsonString = JsonConvert.SerializeObject(block);
            var encoded = Encoding.ASCII.GetBytes(jsonString);
            var sha256Encrypted = SHA256.Create().ComputeHash(encoded);

            return BitConverter.ToString(sha256Encrypted).Replace("-", "").ToLower();
        }

        public static bool IsVaildProof(int lastProof, int proof)
        {
            var guess = Encoding.ASCII.GetBytes($"{lastProof}{proof}");
            var sha256Encrypted = SHA256.Create().ComputeHash(guess);
            var hashString = BitConverter.ToString(sha256Encrypted).Replace("-", "").ToLower();

            return hashString.StartsWith("0000");
        }

        public Block CreateBlock(int proof, string previousHash = null)
        {
            var block = new Block(Chain.Count + 1, DateTime.Now, CurrentTransactions, proof, previousHash ?? Hash(LastBlock));
            CurrentTransactions = new List<Transaction>();
            Chain.Append(block);
            return block;
        }

        public int CreateTransaction(string sender, string recipient, int amount)
        {
            CurrentTransactions.Add(new Transaction(sender, recipient, amount));

            return LastBlock.Index + 1;
        }

        public int ExecutePoW(int lastProof)
        {
            var proof = 0;

            while (!IsVaildProof(lastProof, proof++)) { }

            return proof;
        }
    }
}

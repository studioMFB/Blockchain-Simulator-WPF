using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Blockchain_Simulator_App
{
    /// <summary>
    /// (IBM Developer, 2020)
    /// </summary>
    class Block
    {
        private int _index = 0;
        private DateTime _timeStamp;
        private string _data;
        private string _prevHash = "n/a";
        private string _hash;
        private int _difficulty; // Number of 0 required to be at the begining of a Hash key. 
                                 // The Higher the difficulty, the harder it is to get a suitable Hash key, the slowier it is to mine a Block.
        private int _nonce; // Stands for: "number only used once".
                                   // Default value to be increased untill a suitable Hash key is mined / created.

        public int Index { get { return _index; } set { _index = value; } }
        public DateTime TimeStamp { get { return _timeStamp; } set { _timeStamp = value; } }
        public string Data { get { return _data; } set { _data = value; } }
        public string PrevHash { get { return _prevHash; } set { _prevHash = value; } }
        public string Hash { get { return _hash; } set { _hash = value; } }
        public int Difficulty { get { return _difficulty; } set { _difficulty = value; } }
        public int Nonce { get { return _nonce; } set { _nonce = value; } }

        public Block()
        {
        }
        public Block(int difficulty, string data)
        {
            _difficulty = difficulty;
            _timeStamp = DateTime.Now;
            _data = data;
            _nonce = 0;
        }

        /// <summary>
        /// Write Block details.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string lastHashKey;
            if (_index == 1)
            { lastHashKey = "\n - Genesis Hash Key: "; }
            else
            { lastHashKey = "\n - Previous Hash Key: "; }
            { return ("\n   Block # " + _index + "\n - Time Stamp: " + _timeStamp + "\n - Data: " + _data +
                    "\n - Difficulty: " + _difficulty + "\n - Nonce: " + _nonce +
                    "\n - Hash Key: " + _hash + lastHashKey + _prevHash); }
        }

        /// <summary>
        /// (Docs.microsoft.com, 2020)
        /// Cryptographic function whish from a string creates an hash key of 256 bit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String CalculateHashKey_Sha256()
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                string value = _index.ToString() + this._timeStamp.ToString() + _data + _prevHash.ToString() + _nonce.ToString();
                Byte[] outputBytes = hash.ComputeHash(enc.GetBytes(value));
                foreach (Byte b in outputBytes)
                { stringBuilder.Append(b.ToString("x2")); }
            }
            return stringBuilder.ToString();
        }

        public void MiningBlock(int difficulty)
        {
            string target = new String('0', difficulty);
            do
            {
                _hash = CalculateHashKey_Sha256();
                _nonce++;
            } while ((_hash.Substring(0, difficulty) != target));
        }

    }
}

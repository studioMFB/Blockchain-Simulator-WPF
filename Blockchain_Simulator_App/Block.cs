using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Blockchain_Simulator_App
{
    class Block
    {
        private static int _index = 0;
        private DateTime _timeStamp;
        private string _data;
        private string _prevHash = "n/a";
        private string _hash;
        private static int _nonce; // Stands for: "number only used once".
                                   // Default value to be increased untill a suitable Hash key is mined / created.

        public static int Index { get { return _index; } set { _index = value; } }
        public DateTime TimeStamp { get { return _timeStamp; } set { _timeStamp = value; } }
        public string Data { get { return _data; } set { _data = value; } }
        public string PrevHash { get { return _prevHash; } set { _prevHash = value; } }
        public string Hash { get { return _hash; } set { _hash = value; } }
        public static int Nonce { get { return _nonce; } set { _nonce = value; } }

        public Block(int difficulty, string data)
        {
            Blockchain.Difficulty = difficulty;
            _timeStamp = DateTime.Now;
            _data = data;
            _nonce = 0;
        }
      
        /// <summary>
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

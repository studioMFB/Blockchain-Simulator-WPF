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
        private int _index;
        private DateTime _timeStamp;
        private string _data;
        private string _prevHash = "n/a";
        private string _hash;
        private string _hashTest;
        private static int _nonce; // Default value to be increased untill a suitable Hash key is mined / created.

        public int Index { get { return _index; } set { _index = value; } }
        public DateTime TimeStamp { get { return _timeStamp; } set { _timeStamp = value; } }
        public string Data { get { return _data; } set { _data = value; } }
        public string PrevHash { get { return _prevHash; } set { _prevHash = value; } }
        public string Hash { get { return _hash; } set { _hash = value; } }

        public Block(int index, string data)
        {
            _index = index;
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
            { return ("\n   Index: " + _index + "\n - Date & Time: " + _timeStamp + "\n - Data: " + _data + "\n - Hash Key: " + _hash + lastHashKey + _prevHash); }
        }

        /// <summary>
        /// Cryptographic function whish from a string creates an hash key of 256 bit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String CalculateHashKey_Sha256()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{_index}-{_timeStamp}-{_data}-{_prevHash ?? ""}-{_nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// Cryptographic function whish from a string creates an hash key of 256 bit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String CalculateHashKey_Sha256(string test)
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
                _hashTest = CalculateHashKey_Sha256("test");
                _nonce++;
            } while ((_hash.Substring(0, difficulty) != target));
        }

        public string MiningBlock(int difficulty, string test)
        {
            // _nonce = _nonce - 1;
            _nonce = 0;
            string target = new String('0', difficulty);
            do
            {
                //_hash = CalculateHashKey_Sha256();
                _hash = CalculateHashKey_Sha256("test");
                _nonce++;
            } while ((_hash.Substring(0, difficulty) != target));
            // } while ((_hash.Substring(0, difficulty) != target));

            // return _hash;
            return _hash;
        }

    }
}

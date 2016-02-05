using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ws.sample.utility
{
    class HashUtils
    {
        // Generate signature: SHA1 (currentTimestamp, public_key, private_key)
        public static string createMessageHash(string publicKey, string currentTimestamp, string privateKey)
        {
            string messageHash = "";

            string str = string.Format("{0}{1}{2}", new string[] { currentTimestamp, publicKey, privateKey });

            messageHash = Hash(str);
            return messageHash;
        }

        private static string Hash(string toHash)
        {
            SHA1 _SHA1 = new SHA1Managed();
            ASCIIEncoding _ASCIIEncoding = new ASCIIEncoding();
            Byte[] _ByteArray = _SHA1.ComputeHash(_ASCIIEncoding.GetBytes(toHash));
            return GetAsHexaDecimal(_ByteArray);
        }

        private static string GetAsHexaDecimal(Byte[] bytes)
        {
            StringBuilder _StringBuilder = new StringBuilder();
            string _Temporal;
            for (int x = 0; x < bytes.Length; x++)
            {
                _Temporal = String.Format("{0,2:x}", bytes[x]).Replace(" ", "");
                if (_Temporal.Length == 1)
                    _Temporal = "0" + _Temporal;
                _StringBuilder.Append(_Temporal);
            }
            return _StringBuilder.ToString();
        }
    }
}

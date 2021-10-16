using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    public class Encryption
    {
        /// <summary>
        /// Takes in a string and an encryption key.
        /// Returns the encrypted string.
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encypt(string plainText, byte key)
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = new byte[originalBytes.Length];

            for (int i = 0; i < originalBytes.Length; i++)
            {
                encryptedBytes[i] = (byte) (originalBytes[i] + key);
            }

            return Encoding.UTF8.GetString(encryptedBytes);
        }


        public string Decrypt(string encryptedString, byte key)
        {
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(encryptedString);
            byte[] plainBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length;i++)
            {
                plainBytes[i] = (byte) (encryptedBytes[i] - 7);
            }

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}

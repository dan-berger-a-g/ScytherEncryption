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
        public string Encypt(string plainText, byte[] key)
        {
            //Allows the encryption to be able to understand what to encrypt, which is text
            byte[] originalBytes = Encoding.UTF8.GetBytes(plainText);
            //tells the encryption to offset the text by a set length
            byte[] encryptedBytes = new byte[originalBytes.Length];

            //Establishes length by offseting the text by one until it is offest by the right number
            for (int i = 0; i < originalBytes.Length;  i++)
            {
                // old way
                // encryptedBytes[i] = (byte)(originalBytes[i] + key[0]);

                // new way
                encryptedBytes[i] = (byte)(originalBytes[i] + key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(encryptedBytes);
        }


        public string Decrypt(string encryptedString, byte[] key)
        {
            //Everything above but reversed
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(encryptedString);
            byte[] plainBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                // old way
                // plainBytes[i] = (byte)(encryptedBytes[i] - key[0]);

                // new way
                plainBytes[i] = (byte)(encryptedBytes[i] - key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}



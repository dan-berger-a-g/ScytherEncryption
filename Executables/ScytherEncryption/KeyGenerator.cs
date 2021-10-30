using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    public class KeyGenerator
    {

        /// <summary>
        /// Reads the contents of an encryption key file as bytes
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>key bytes</returns>
        /// <exception cref="ArgumentException">File not found</exception>
        public byte[] ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Key file \"{0}\" does not exist", filePath);
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Generates a random encryption key.
        /// Key is 256 bytes.
        /// </summary>
        /// <returns>key bytes</returns>
        public byte[] Generate()
        {
            byte[] keyBytes = new byte[256];
            Random rnd = new Random();

            for (int i = 0; i < keyBytes.Length; i++)
            {
                byte randomByte = (byte)rnd.Next(256);
                keyBytes[i] = randomByte;
            }

            return keyBytes;
        }

        public bool WriteToFile(byte[] key, string filePath, out string errorMessage)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllBytes(filePath, key);
                errorMessage = null;
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
        }
    }
}

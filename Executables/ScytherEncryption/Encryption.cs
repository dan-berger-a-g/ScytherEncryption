using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    public class Encryption
    {

        /// <summary>
        /// Attempts to encrypt the contents of the given file, using the given key.
        /// Deletes the original file and writes the encrypted contents to the same file path with a .encrypted extension
        /// Returns true if the operation was successful.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="keyFilePath"></param>
        /// <param name="encryptedFile"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool EncryptFile(string filePath, string keyFilePath, out string encryptedFile, out string errorMessage)
        {
            try
            {
                var keyGenerator = new KeyGenerator();

                var key = keyGenerator.ReadFromFile(keyFilePath);
                var fileBytes = File.ReadAllBytes(filePath);

                var encryptedBytes = Encypt(fileBytes, key);

                File.Delete(filePath);

                encryptedFile = filePath + ".encrypted";
                File.WriteAllBytes(encryptedFile, encryptedBytes);
                errorMessage = null;
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                encryptedFile = null;
                return false;
            }
        }

        /// <summary>
        /// Takes in a string and an encryption key.
        /// Returns the encrypted string.
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Encypt(byte[] decryptedBytes, byte[] key)
        {
            // initialize an array to hold the encrypted bytes
            byte[] encryptedBytes = new byte[decryptedBytes.Length];

            // encrypt each byte using the key bytes
            for (int i = 0; i < decryptedBytes.Length;  i++)
            {
                // old way
                // encryptedBytes[i] = (byte)(originalBytes[i] + key[0]);

                // new way
                encryptedBytes[i] = (byte)(decryptedBytes[i] + key[i % key.Length]);
            }

            return encryptedBytes;
        }


        public bool DecryptFile(string filePath, string keyPath, out string decryptedFile, out string errorMessage)
        {
            try
            {
                if (Path.GetExtension(filePath) != ".encrypted")
                {
                    throw new InvalidOperationException("Can only decrypt files ending in a .encrypted extension");
                }

                var keyGenerator = new KeyGenerator();
                var key = keyGenerator.ReadFromFile(keyPath);

                var fileBytes = File.ReadAllBytes(filePath);

                var decryptedBytes = Decrypt(fileBytes, key);

                decryptedFile = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
                File.WriteAllBytes(decryptedFile, decryptedBytes);
                File.Delete(filePath);
                errorMessage = null;
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                decryptedFile = null;
                return false;
            }
        }


        public byte[] Decrypt(byte[] encryptedBytes, byte[] key)
        {
            // initialize an array to hold the decrypted bytes
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            // decrypt each byte using the key bytes
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                // old way
                // plainBytes[i] = (byte)(encryptedBytes[i] - key[0]);

                // new way
                decryptedBytes[i] = (byte)(encryptedBytes[i] - key[i % key.Length]);
            }

            return decryptedBytes;
        }
    }
}



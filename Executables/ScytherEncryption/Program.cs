using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{

    class Program
    {
        static void Main(string[] args)
        {
            // These are variables that we will need later
            string userFile;
            string keyFile;
            byte[] fileContents;
            byte[] key;

            // This object handles creating new keys, reading keys from a file, or writing keys to a file
            var keyGenerator = new KeyGenerator();

            // This object handles encrypting and decrypting files using a key
            var encryptor = new Encryption();

            Console.WriteLine("Enter a command:"); // Ask the user to enter a command
            UserCommand cmd = GetUserCommand(); // parse their input into a UserCommand enum

            // Until the user quits, keep asking for the next command, and process it
            while (cmd != UserCommand.Quit)
            {
                switch (cmd)
                {
                    case UserCommand.Encrypt:
                        userFile = GetUserFile("Enter the path to the file to encrypt", ".txt", true);
                        keyFile = GetUserFile("Enter the path to the encryption key", ".key", true);
                        if (encryptor.EncryptFile(userFile, keyFile, out string encryptedFile, out string errorMessage))
                        {
                            Console.WriteLine("Successfully encrypted file, new file path: " + encryptedFile);
                        }
                        else
                        {
                            Console.WriteLine("Unable to encrypt file: " + errorMessage);
                        }
                        break;
                    case UserCommand.Decrypt:
                        userFile = GetUserFile("Enter the path to the .encrypted file to decrypt", ".encrypted", true);
                        keyFile = GetUserFile("Enter the path to the encryption key", ".key", true);
                        if (encryptor.DecryptFile(userFile, keyFile, out string decryptedFile, out errorMessage))
                        {
                            Console.WriteLine("Successfully decrypted file, new file path: " + decryptedFile);
                        }
                        else
                        {
                            Console.WriteLine("Unable to decrypt file: " + errorMessage);
                        }
                        break;
                    case UserCommand.Help:
                        PrintHelp();
                        break;
                    case UserCommand.GenerateKey:
                        string filePath = GetUserFile("Enter the path to the key file", ".key", false);
                        key = keyGenerator.Generate();
                        if (keyGenerator.WriteToFile(key, filePath, out errorMessage))
                        {
                            Console.WriteLine("Key successfully generated at \"{0}\"", filePath);
                        }
                        else
                        {
                            Console.WriteLine("Unable to generate key: " + errorMessage);
                        }
                        break;
                    case UserCommand.Unknown:
                        Console.WriteLine("Unknown command.\r\nEnter a commmand, use H for help:");
                        break;
                }

                Console.WriteLine("Enter a command:");
                cmd = GetUserCommand();
            }
        }

        /// <summary>
        /// Gets user input from the console, then turns it into a UserCommand enum.
        /// </summary>
        /// <returns></returns>
        static UserCommand GetUserCommand()
        {
            string userInput = Console.ReadLine();

            switch (userInput.ToLower())
            {
                case "q":
                case "quit":
                    return UserCommand.Quit;
                case "e":
                case "encrypt":
                    return UserCommand.Encrypt;
                case "d":
                case "decrypt":
                    return UserCommand.Decrypt;
                case "h":
                case "help":
                    return UserCommand.Help;
                case "g":
                case "generate key":
                    return UserCommand.GenerateKey;
            }

            return UserCommand.Unknown;
        }

        //Tells user what do do if they type help
        static void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("\tQ or QUIT to Exit");
            Console.WriteLine("\tE or ENCRYPT to Encrypt a file");
            Console.WriteLine("\tD or DECRYPT to Decrypt a file");
            Console.WriteLine("\tH or HELP to print this message");
        }

        //Tells the user and the console how to type in what file the user wants to encrypt, and tells console what to do if the file was typed in wrong or doesn't exist
        static string GetUserFile(string prompt, string extension, bool checkIfExists)
        {
            Console.WriteLine(prompt);
            string userFile = Console.ReadLine().Trim().Replace("\"", "");

            while ((checkIfExists == true && File.Exists(userFile) == false) || Path.GetExtension(userFile) != extension)
            {
                Console.WriteLine("File \"{0}\" not found or invalid. Must end with extension {1}", userFile, extension);
                Console.WriteLine("Enter the file path:");
                userFile = Console.ReadLine().Trim().Replace("\"", "");
            }

           return userFile;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    enum UserCommand
    {
        Unknown,
        Help,
        Quit,
        Encrypt,
        Decrypt
    }

    class Program
    {
        const int KEY = 7;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a command:");
            UserCommand cmd = GetUserCommand();

            var encryptor = new Encryption();
            string userFile;
            string fileContents;

            while (cmd != UserCommand.Quit)
            {
                switch (cmd)
                {
                    case UserCommand.Encrypt:
                        userFile = GetUserFile(".txt");
                        fileContents = File.ReadAllText(userFile);
                        string encrypted = encryptor.Encypt(fileContents, KEY);

                        File.Delete(userFile);
                        File.WriteAllText(userFile + ".encrypted", encrypted);
                        break;
                    case UserCommand.Decrypt:
                        userFile = GetUserFile(".encrypted");
                        fileContents = File.ReadAllText(userFile);
                        string plainText = encryptor.Decrypt(fileContents, KEY);

                        File.Delete(userFile);
                        string txtFileName = Path.GetFileNameWithoutExtension(userFile);
                        userFile = Path.Combine(Path.GetDirectoryName(userFile), txtFileName);
                        File.WriteAllText(userFile, plainText);
                        break;
                    case UserCommand.Help:
                        PrintHelp();
                        break;
                    case UserCommand.Unknown:
                        Console.WriteLine("Unknown command.\r\nEnter a commmand, use H for help:");
                        break;
                }

                Console.WriteLine("Enter a command:");
                cmd = GetUserCommand();
            }
        }

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
            }

            return UserCommand.Unknown;
        }

        static void PrintHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("\tQ or QUIT to Exit");
            Console.WriteLine("\tE or ENCRYPT to Encrypt a file");
            Console.WriteLine("\tD or DECRYPT to Decrypt a file");
            Console.WriteLine("\tH or HELP to print this message");
        }

        static string GetUserFile(string extension)
        {
            Console.WriteLine("Enter the file path:");
            string userFile = Console.ReadLine().Trim().Replace("\"", "");

            while (File.Exists(userFile) == false || Path.GetExtension(userFile) != extension)
            {
                Console.WriteLine("File \"{0}\" not found or invalid", userFile);
                Console.WriteLine("Enter the file path:");
                userFile = Console.ReadLine().Trim().Replace("\"", "");
            }

            return userFile;
        }
    }
}

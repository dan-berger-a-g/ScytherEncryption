using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    //Establish commands that the user can use
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



        static void Main(string[] args)
        {
            //Generate key and implement that generated key
            var keyGenerator = new KeyGenerator();
            byte[] KEY = keyGenerator.Generate();
       
            //Tell console to read the user's command from above
            Console.WriteLine("Enter a command:");
            UserCommand cmd = GetUserCommand();

            //Tell encryption what text to encrypt
            var encryptor = new Encryption();
            string userFile;
            string fileContents;

            //Tells the console what to do in the case of each of the user's commands
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
                        Console.WriteLine("File successfully encrypted " + userFile + ".encrypted");
                        break;
                    case UserCommand.Decrypt:
                        userFile = GetUserFile(".encrypted");
                        fileContents = File.ReadAllText(userFile);
                        string plainText = encryptor.Decrypt(fileContents, KEY);

                        File.Delete(userFile);
                        string txtFileName = Path.GetFileNameWithoutExtension(userFile);
                        userFile = Path.Combine(Path.GetDirectoryName(userFile), txtFileName);
                        File.WriteAllText(userFile, plainText);
                        Console.WriteLine("File successfully decrypted " + userFile);
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

        //Tells the console to repeat last steps so that the console doesn't close after the user does one command
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

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
            Console.WriteLine("Select a file to encrypt:");

            string userFile = Console.ReadLine().Trim().Replace("\"", "");

            while (!File.Exists(userFile))
            {
                Console.WriteLine("File \"{0}\" not found", userFile);
                Console.WriteLine("Select a file to encrypt:");
                userFile = Console.ReadLine().Trim().Replace("\"", "");
            }

            PrintFile(userFile);

            Console.WriteLine("\r\nPress enter to exit...");
            Console.ReadLine();
        }

        static void PrintFile(string filePath)
        {
            string contents = File.ReadAllText(filePath);
            Console.WriteLine("\r\nFile contents:");
            Console.WriteLine(contents);
        } 
    }
}

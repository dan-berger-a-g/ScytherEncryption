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

            string contents = File.ReadAllText(userFile);
            Console.WriteLine("\r\nFile contents:");
            Console.WriteLine(contents);

            Console.WriteLine("\r\nPress enter to exit...");
            Console.ReadLine();
        }
    }
}

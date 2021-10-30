using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    /// <summary>
    /// Commands that the user can input
    /// </summary>
    enum UserCommand
    {
        Unknown,
        Help,
        Quit,
        Encrypt,
        Decrypt,
        GenerateKey
    }
}

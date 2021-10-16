using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScytherEncryption
{
    [TestFixture]
    class EncryptionTests
    {
        Encryption o;

        [SetUp]
        public void SetUp()
        {
            o = new Encryption();
        }

      
        [Test]
        public void TestEncryption()
        {
            string s = "Hello, world!" + Environment.NewLine + "foo line";
            byte key = 7;

            string encrypted = o.Encypt(s, key);
            Assert.AreNotEqual(s, encrypted);

            byte[] original = Encoding.UTF8.GetBytes(s);
            byte[] enrypted = Encoding.UTF8.GetBytes(encrypted);

            Assert.AreEqual(original.Length, encrypted.Length);

            for (int i = 0; i < original.Length; i++)
            {
                Assert.AreEqual(original[i] + 7, encrypted[i], "Problem at index " + i);
            }

            string plain = o.Decrypt(encrypted, key);
            Assert.AreEqual(s, plain);
        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScytherEncryption
{
    [TestFixture]
    class KeyGeneratorTests
    {
        KeyGenerator o;
        string tempFile;

        [SetUp]
        public void SetUp()
        {
            o = new KeyGenerator();

            tempFile = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(tempFile);
        }

        [Test]
        public void TestGenerate()
        {
            var key1 = o.Generate();
            Assert.AreEqual(256, key1.Length);

            var key2 = o.Generate();
            Assert.AreEqual(256, key1.Length);

            bool areSame = true;

            key1 = new byte[] { 1, 5, 7, 4 }; // 4
            key2 = new byte[] { 1, 2, 7, 9, 10 }; // 5

            for (int i = 0; i < key1.Length && i < key2.Length; i++)
            {
                if (key1[i] != key2[i])
                {
                    areSame = false;
                    break;
                }
            }

            Assert.IsFalse(areSame);
        }

        [Test]
        public void TestWriteToFile()
        {
            var key = o.Generate();

            Assert.IsTrue(o.WriteToFile(key, tempFile, out string errorMessage));

            var keyFromFile = o.ReadFromFile(tempFile);

            Assert.AreEqual(key.Length, keyFromFile.Length);

            for (int i = 0; i < key.Length && i < keyFromFile.Length; i++)
            {
                Assert.AreEqual(key[i], keyFromFile[i]);
            }
        }

        [Test]
        public void TestReadFromFileNotExist()
        {
            string filePath = @"C:\this\is\not\a\file.txt";
            Assert.Throws<ArgumentException>(delegate { o.ReadFromFile(filePath); });
        }

        [Test]
        public void TestWriteToInvalidFile()
        {
            var key = o.Generate();

            string invalidPath = @"C:\path::1\file.txt";

            Assert.IsFalse(o.WriteToFile(key, invalidPath, out string errorMessage));

            Console.WriteLine(errorMessage);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }

    }
}

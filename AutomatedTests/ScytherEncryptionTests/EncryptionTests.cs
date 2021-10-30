using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ScytherEncryption
{
    [TestFixture]
    class EncryptionTests
    {
        Encryption o;
        string pathToTestDirectory;

        string tempFile;

        [SetUp]
        public void SetUp()
        {
            o = new Encryption();

            pathToTestDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files");

            tempFile = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(tempFile);
        }

        [Test]
        public void TestEncryptFile()
        {
            var plainTextFileContents = File.ReadAllBytes(Path.Combine(pathToTestDirectory, "PlainTextFile.txt"));
            File.WriteAllBytes(tempFile, plainTextFileContents);

            var keyFile = Path.Combine(pathToTestDirectory, "Key.key");

            Assert.IsTrue(File.Exists(tempFile));
            Assert.IsTrue(o.EncryptFile(tempFile, keyFile, out string encryptedFile, out string errorMessage));
            Assert.IsFalse(File.Exists(tempFile));
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));

            Assert.AreEqual(tempFile + ".encrypted", encryptedFile);
            Assert.IsTrue(File.Exists(encryptedFile));

            FileAssert.AreEqual(encryptedFile, Path.Combine(pathToTestDirectory, "PlainTextFile.txt.encrypted"));

            tempFile = encryptedFile;
        }

        [Test]
        public void TestEncryptFileNotValid()
        {
            var otherTempFile = Path.GetTempFileName();
            File.Delete(otherTempFile);

            Assert.IsFalse(File.Exists(otherTempFile));

            var keyFile = Path.Combine(pathToTestDirectory, "Key.key");

            Assert.IsFalse(o.EncryptFile(otherTempFile, keyFile, out string encryptedFile, out string errorMessage));

            Console.WriteLine(errorMessage);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
            Assert.IsTrue(string.IsNullOrEmpty(encryptedFile));
        }

        [Test]
        public void TestDecryptFile()
        {
            var encryptedFileBytes = File.ReadAllBytes(Path.Combine(pathToTestDirectory, "PlainTextFile.txt.encrypted"));
            File.WriteAllBytes(tempFile + ".encrypted", encryptedFileBytes);

            var keyFile = Path.Combine(pathToTestDirectory, "Key.key");

            File.Delete(tempFile);
            Assert.IsTrue(File.Exists(tempFile + ".encrypted"));
            Assert.IsFalse(File.Exists(tempFile));
            Assert.IsTrue(o.DecryptFile(tempFile + ".encrypted", keyFile, out string decryptedFile, out string errorMessage));
            Assert.IsFalse(File.Exists(tempFile + ".encrypted"));

            Assert.AreEqual(tempFile, decryptedFile);
            Assert.IsTrue(File.Exists(tempFile));

            FileAssert.AreEqual(Path.Combine(pathToTestDirectory, "PlainTextFile.txt"), tempFile);
        }

        [Test]
        public void TestDecryptFileWrongExtension()
        {
            var encryptedFileBytes = File.ReadAllBytes(Path.Combine(pathToTestDirectory, "PlainTextFile.txt.encrypted"));
            File.WriteAllBytes(tempFile, encryptedFileBytes);

            var keyFile = Path.Combine(pathToTestDirectory, "Key.key");

            Assert.IsFalse(o.DecryptFile(tempFile, keyFile, out string decryptedFile, out string errorMessage));

            Assert.IsTrue(File.Exists(tempFile));

            Console.WriteLine(errorMessage);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }

        [Test]
        public void TestEncrypt()
        {
            var file = Path.Combine(pathToTestDirectory, "PlainTextFile.txt");
            var keyFile = Path.Combine(pathToTestDirectory, "Key.key");

            var key = (new KeyGenerator()).ReadFromFile(keyFile);

            var originalBytes = File.ReadAllBytes(file);

            var encryptedBytes = o.Encypt(originalBytes, key);

            var decrytpedBytes = o.Decrypt(encryptedBytes, key);

            Assert.AreEqual(originalBytes.Length, encryptedBytes.Length);
            Assert.AreEqual(originalBytes.Length, decrytpedBytes.Length);

            for (int i = 0; i < originalBytes.Length; i++)
            {
                Assert.AreEqual(originalBytes[i], decrytpedBytes[i]);
            }

            bool somethingChanged = false;

            for (int i = 0; i < originalBytes.Length; i++)
            {
                if (originalBytes[i] != encryptedBytes[i])
                {
                    somethingChanged = true;
                    break;
                }
            }

            Assert.IsTrue(somethingChanged);
        }

     
    }
}

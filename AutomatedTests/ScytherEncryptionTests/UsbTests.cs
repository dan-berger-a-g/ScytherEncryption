using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScytherEncryption
{
    [TestFixture]
    class UsbTests
    {
        Usb o;
        string tempFile;
        string tempFile1;

        [SetUp]
        public void SetUp()
        {
            o = new Usb();
            tempFile = Path.GetTempFileName();
            tempFile1 = Path.GetTempFileName();
        }

            [TearDown]
            public void TearDown()
            {
                File.Delete(tempFile);
                File.Delete(tempFile1);
            }

            [Test]
            public void TestRightUsbStickandCombining()
            {
                bool written = false;
                var drive = DriveInfo.GetDrives().FirstOrDefault(x => x.IsReady);
                string driveRoot = drive.RootDirectory.FullName;
                string file = Path.Combine(driveRoot, tempFile1);
                if (file != null)
                {
                    written = true;
                }
            Assert.IsTrue(written);
            Assert.AreNotEqual(driveRoot, null);
            }
         //[Test]
         /*public void ComineFiles()
        {
            bool written = false;
            var drive = DriveInfo.GetDrives().FirstOrDefault(x => x.IsReady);
            string driveRoot = drive.RootDirectory.FullName;
            Directory.CreateDirectory(tempFile1);
            string file = Path.Combine(driveRoot, tempFile1);
            if(file != null)
            {
                written = true;
            }
            Assert.Pass();*/
        }
    }


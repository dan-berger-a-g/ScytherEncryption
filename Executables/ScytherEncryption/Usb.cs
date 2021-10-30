using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScytherEncryption
{
    public class Usb
    {

        /// <summary>
        /// Attempts to find an attached removable drive with a matching volume label.
        /// Returns true if a match was found
        /// </summary>
        /// <param name="volumeLabel"></param>
        /// <param name="usb"></param>
        /// <returns></returns>
        public bool FindRemovableDrive(string volumeLabel, out DriveInfo usb)
        {
            var drives = DriveInfo.GetDrives();

            usb = drives.FirstOrDefault(x => x.IsReady && x.DriveType == DriveType.Removable && x.VolumeLabel.ToLower() == volumeLabel.ToLower());
            return usb != null;
        }

        /// <summary>
        /// Returns the path to the encryption key for the given drive
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        public string GetKeyPath(DriveInfo drive)
        {
            return Path.Combine(drive.RootDirectory.FullName, KeyGenerator.KEY_NAME);
        }
    }
}

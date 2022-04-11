using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaedeInstaller
{
    internal class ZipArchiveHelper
    {
        public static void ExtractToDirectory(string archiveFileName, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
                ZipFile.ExtractToDirectory(archiveFileName, destinationDirectoryName);
            else
            {
                using (var archive = ZipFile.OpenRead(archiveFileName))
                {
                    foreach (var file in archive.Entries)
                    {
                        var completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                        var directory = Path.GetDirectoryName(completeFileName);

                        if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
                            Directory.CreateDirectory(directory);

                        if (file.Name != "")
                            file.ExtractToFile(completeFileName, true);
                    }
                }
            }
        }
    }
}

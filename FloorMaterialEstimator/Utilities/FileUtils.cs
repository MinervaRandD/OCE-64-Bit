using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FileUtils
    {
        public static bool IsFileLocked(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }
                // Try to open for *read/write* with *no sharing*.
                var fs = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.ReadWrite,
                    FileShare.None);

                fs.Close();

                return false;           // success ➜ not locked
            }
            catch (IOException ex)
            {
                return true;            // IOException ➜ another process has it open
            }
        }

    }
}

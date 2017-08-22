using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libtruncate
{
    public class Truncate
    {
        public static void TruncateFiles(bool noCreate, bool quiet, long size, List<string> files)
        {
            FileMode fileMode = FileMode.OpenOrCreate;
            if (noCreate)
                fileMode = FileMode.Open;

            foreach (string f in files)
            {
                string fullPath = Path.GetFullPath(f);
                try
                {
                    using (FileStream fs = new FileStream(fullPath, fileMode))
                    {
                        fs.SetLength(size);
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                    }
                }
                catch (FileNotFoundException)
                {
                    if (!quiet)
                        Console.WriteLine($"File not found: {Path.GetFullPath(f)}");
                }
            }
        }

        public static void TruncateFilesAsync(bool noCreate, bool quiet, long size, List<string> files)
        {

        }

        public static void ParalelleTruncateFiles(bool noCreate, bool quiet, long size, List<string> files)
        {

        }

        public static void ParalelleTruncateFilesAsync(bool noCreate, bool quiet, long size, List<string> files)
        {

        }
    }
}

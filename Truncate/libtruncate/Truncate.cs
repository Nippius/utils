using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LibTruncate
{
    public class Truncate
    {
        public static void TruncateFiles(FileMode fileMode, bool quiet, long size, List<string> files)
        {
            foreach (string f in files)
            {
                string fullPath = Path.GetFullPath(f);
                TruncateFile(fileMode, size, quiet, fullPath);
            }
        }

        public static void ParallelTruncateFiles(FileMode fileMode, bool quiet, long size, List<string> files)
        {
            Parallel.ForEach(files, (file) =>
            {
                string fullPath = Path.GetFullPath(file);
                TruncateFile(fileMode, size, quiet, fullPath);
            });
        }

        private static void TruncateFile(FileMode fileMode, long size, bool quiet, string fileFullPath)
        {
            try
            {
                using (FileStream fs = new FileStream(fileFullPath, fileMode))
                {
                    fs.SetLength(size);
                    // Make sure that the changes take effect when the stream is
                    // flushed to disk.
                    fs.Seek(fs.Length, SeekOrigin.Begin);
                }
            }
            catch (FileNotFoundException)
            {
                if (!quiet)
                    Console.WriteLine($"File not found: {fileFullPath}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LibTruncate
{
    /// <summary>
    /// Available options for truncating files.
    /// </summary>
    public class TruncateOptions
    {
        public FileMode FileMode { get; set; } = FileMode.OpenOrCreate;
        public bool Quiet { get; set; }
        public long Size { get; set; }
    }

    /// <summary>
    /// Contains static methods for truncating a list of files.
    /// </summary>
    public class Truncate
    {
        public static void TruncateFiles(TruncateOptions opt, List<string> files)
        {
            foreach (string f in files)
            {
                string fullPath = Path.GetFullPath(f);
                TruncateFile(opt, fullPath);
            }
        }

        public static void ParallelTruncateFiles(TruncateOptions opt, List<string> files)
        {
            Parallel.ForEach(files, (file) =>
            {
                string fullPath = Path.GetFullPath(file);
                TruncateFile(opt, fullPath);
            });
        }

        private static void TruncateFile(TruncateOptions opt, string fileFullPath)
        {
            if (opt == null)
                throw new ArgumentNullException(nameof(opt));
            try
            {
                using (FileStream fs = new FileStream(fileFullPath, opt.FileMode))
                {
                    fs.SetLength(opt.Size);
                    // Make sure that the changes take effect when the stream is
                    // flushed to disk.
                    fs.Seek(fs.Length, SeekOrigin.Begin);
                }
            }
            catch (FileNotFoundException)
            {
                if (!opt.Quiet)
                    Console.WriteLine($"File not found: {fileFullPath}");
            }
        }
    }
}

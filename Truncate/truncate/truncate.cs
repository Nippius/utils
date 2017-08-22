using clipr;
using clipr.Core;
using libtruncate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace truncate
{
    class truncate
    {
        [ApplicationInfo(Description = "Truncate one or more files to specified size.")]
        private class Options
        {
            [NamedArgument('q', "quiet", Action = ParseAction.StoreTrue,
                Description = "Do not show any output.")]
            public bool Quiet { get; set; }

            [NamedArgument('c', "no-create", Action = ParseAction.StoreTrue,
                Description = "Do not create any files (but truncate existing).")]
            public bool NoCreate { get; set; }

            [NamedArgument('s', "size",
                Description = "Set or adjust the file size by SIZE bytes. Defaults to 0 if ommited.")]
            public long Size { get; set; }

            [PositionalArgument(0, MetaVar = "FILES",
                NumArgs = 1,
                Constraint = NumArgsConstraint.AtLeast,
                Description = "Files to truncate.")]
            public List<string> FileNames { get; set; }
        }

        

        static void Main(string[] args)
        {
            try
            {
                Options opt = CliParser.Parse<Options>(args);
                Truncate.TruncateFiles(opt.NoCreate, opt.Quiet, opt.Size, opt.FileNames);
            }
            catch (ParseException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ParserExit)
            {
                // This is thrown when the --help or --version parameters are passed
                // to the parser so we can safely ignore.
                return;
            }
        }
    }
}

using LibTruncate;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace TruncateTests
{
    [TestClass]
    public class TruncateTests
    {
        [TestMethod]
        public void TestFileIsntCreatedIfItDoesntExist()
        {

        }

        [TestMethod]
        public void TestFileIsCreatedIfItDoesntExist()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestThrowsExceptionOnNegativeSize()
        {
            long negativeSize = -42;
            string fileName = Path.GetRandomFileName();
            List<string> files = new List<string>();
            files.Add(fileName);

            try
            {
                Truncate.TruncateFiles(FileMode.OpenOrCreate, false, negativeSize, files);
            }
            finally
            {
                new FileInfo(fileName).Delete();
            }
        }

        [TestMethod]
        public void TestNewFileSizeIsCorrect()
        {

        }
    }
}

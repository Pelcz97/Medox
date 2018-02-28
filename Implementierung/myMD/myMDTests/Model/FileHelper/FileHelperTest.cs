using myMD.Model.FileHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMDTests.Model.FileHelper
{
    public class FileHelperTest
    {
        private static IFileHelper helper = new TestFileHelper();

        private static readonly string SAMPLE = "test.hl7";

        [Test]
        public void WriteFromBytesTest()
        {
            string file = helper.GetLocalFilePath(SAMPLE);
            byte[] data = File.ReadAllBytes(file);
            string newFile = helper.WriteLocalFileFromBytes(Path.GetExtension(file), data);
            Assert.AreEqual(data, File.ReadAllBytes(newFile));
        }
    }
}

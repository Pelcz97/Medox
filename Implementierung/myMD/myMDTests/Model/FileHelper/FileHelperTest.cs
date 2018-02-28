﻿using myMD.Model.FileHelper;
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
        private static TestFileHelper helper = new TestFileHelper();

        private static readonly string SAMPLE = "test.hl7";

        private static string Path => helper.GetLocalFilePath(SAMPLE);

        [Test]
        public void WriteFromBytesTest()
        {
            byte[] data = File.ReadAllBytes(Path);
            string newFile = helper.WriteLocalFileFromBytes(System.IO.Path.GetExtension(Path), data);
            Assert.AreEqual(data, File.ReadAllBytes(newFile));
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(helper.LastWrittenPath);
        }
    }
}

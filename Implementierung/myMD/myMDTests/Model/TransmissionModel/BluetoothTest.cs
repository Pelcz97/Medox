using System;
using System.Collections.Generic;
using myMD.Model.TransmissionModel;
using NUnit.Framework;

namespace myMDTests.Model.TransmissionModel
{
    [TestFixture]
    public class BluetoothTest
    {
        readonly Bluetooth test = new Bluetooth();

        private byte[] simpleNumberArray1 = new byte[3] { 1, 2, 3 };
        private byte[] simpleNumberArray2 = new byte[3] { 4, 5, 6 };
        private byte[] simpleNumberArray3 = new byte[3] { 7, 8, 9 };
        private byte[] simpleNumberArrayCombined = new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };


        [Test]
        public void ListToArrayTest(){

            List<byte[]> testList = new List<byte[]>();
            testList.Add(simpleNumberArray1);
            testList.Add(simpleNumberArray2);
            testList.Add(simpleNumberArray3);

            byte[] result = test.ListToArray(testList);
            Assert.AreEqual(result, simpleNumberArrayCombined);
        }    
    }
}

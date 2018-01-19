using NUnit.Framework;
using myMD.Model.DataModel;
using System;
using System.Linq;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class DoctorsLetterGroupTest
    {
        private static DateTime[] DATES = new DateTime[] {
            new DateTime(2017, 12, 24),
            new DateTime(2015, 1, 1),
            new DateTime(2018, 1, 1),
            new DateTime(1998, 8, 6),
            new DateTime(2003, 3, 15)
        };
        private DoctorsLetter[] letters;
        private DoctorsLetterGroup group;

        [SetUp]
        public void SetUp()
        {
            letters = new DoctorsLetter[DATES.Length];
            group = new DoctorsLetterGroup();
            for (int i = 0; i < DATES.Length; ++i)
            {
                letters[i] = new DoctorsLetter { Date = DATES[i] };
                group.Add(letters[i]);
            }
        }

        //[Test]
        public void DateTest()
        {
            Assert.AreEqual(group.Date, DATES.Min());
            Assert.AreEqual(group.LastDate, DATES.Max());           
        }

        //[Test]
        public void AddTest()
        {
            foreach (DoctorsLetter letter in letters)
            {
                Assert.IsTrue(letter.Groups.Contains(group));
                Assert.IsTrue(group.DoctorsLetters.Contains(letter));
            }
        }

        //[Test]
        public void DeleteTest()
        {
            group.Delete();
            foreach(DoctorsLetter letter in letters)
            {
                Assert.IsFalse(letter.Groups.Contains(group));
                Assert.IsFalse(group.DoctorsLetters.Contains(letter));
            }
        }

        /*[TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]*/
        public void RemoveTest(int i)
        {
            group.Remove(letters[i]);
            Assert.IsFalse(group.DoctorsLetters.Contains(letters[i]));
            Assert.IsFalse(letters[i].Groups.Contains(group));
        }

        [TearDown]
        public void TearDown()
        {
            letters = null;
            group = null;
        }
    }
}

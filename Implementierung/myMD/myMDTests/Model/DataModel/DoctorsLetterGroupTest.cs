using NUnit.Framework;
using myMD.Model.DataModel;
using System;
using System.Linq;
using myMDTests.Model.EntityFactory;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class DoctorsLetterGroupTest
    {
        private static readonly int COUNT = 10;
        private static readonly int[] INDICES = Numbers;
        private DateTime[] dates;
        private DoctorsLetter[] letters;
        private DoctorsLetterGroup group;

        public static int[] Numbers
        {
            get
            {
                int[] numbers = new int[COUNT];
                for (int i = 0; i < COUNT; ++i)
                {
                    numbers[i] = i;
                }
                return numbers;
            }
        }

        [SetUp]
        public void SetUp()
        {
            var fac = new RandomEntityFactory();
            dates = new DateTime[COUNT];
            letters = new DoctorsLetter[dates.Length];
            group = fac.Group();
            for (int i = 0; i < COUNT; ++i) {
                letters[i] = fac.Letter();
                dates[i] = letters[i].Date;
                group.Add(letters[i]);
            }       
        }

        [Test]
        public void DateTest()
        {
            Assert.AreEqual(group.Date, dates.Min());
            Assert.AreEqual(group.LastDate, dates.Max());           
        }

        [Test]
        public void AddTest()
        {
            foreach (DoctorsLetter letter in letters)
            {
                Assert.IsTrue(letter.DatabaseGroups.Contains(group));
                Assert.IsTrue(group.DatabaseLetters.Contains(letter));
            }
        }

        [Test]
        public void CompareLetterTest()
        {
            Array.Sort(dates);
            Array.Sort(letters);
            DateTime[] letterDates = new DateTime[COUNT];
            for (int i = 0; i < COUNT; ++i) {
                letterDates[i] = letters[i].Date;
            }
            Assert.True(letterDates.SequenceEqual(dates));
        }

        [Test]
        public void FirstLastDateTest()
        {
            Array.Sort(dates);
            Assert.AreEqual(dates.First(), group.Date);
            Assert.AreEqual(dates.Last(), group.LastDate);
        }

        [Test]
        public void DeleteTest()
        {
            group.Delete();
            foreach(DoctorsLetter letter in letters)
            {
                Assert.IsFalse(letter.Groups.Contains(group));
                Assert.IsFalse(group.DoctorsLetters.Contains(letter));
            }
        }

        [Test, TestCaseSource("INDICES")]
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
            dates = null;
        }
    }
}

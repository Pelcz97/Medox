﻿using NUnit.Framework;
using myMD.Model.DataModel;
using myMDTests.Model.EntityFactory;
using myMD.ModelInterface.DataModelInterface;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class MedicationTest
    {
        private Medication med;
        private RandomEntityFactory fac;
        private IDoctorsLetter letter;

        [SetUp]
        public void SetUp()
        {
            fac = new RandomEntityFactory();
            med = fac.Medication();
            letter = fac.ILetter();
        }

        [Test]
        public void SameEqualTest()
        {
            Assert.IsTrue(med.Equals(med));
        }

        [Test]
        public void CopiedEqualTest()
        {
            Medication copy = Copy(med);
            Assert.IsTrue(med.Equals(copy));
            Assert.AreEqual(med.GetHashCode(), copy.GetHashCode());
        }

        [Test]
        public void NullEqualTest()
        {
            Assert.IsFalse(med.Equals(null));
        }

        [Test]
        public void NotEqualTest()
        {
            Medication changedMed = Copy(med);
            changedMed.Name += "d";
            Assert.IsFalse(med.Equals(changedMed));
        }

        [Test]
        public void LetterTest()
        {
            med.AttachToLetter(letter);
            Assert.IsTrue(med.DoctorsLetter.Equals(letter));
            med.DisattachFromLetter(letter);
            Assert.IsNull(med.DoctorsLetter);
        }

        [Test]
        public void DeleteTest()
        {
            med.AttachToLetter(letter);
            Assert.IsTrue(letter.Medication.Contains(med));
            med.Delete();
            Assert.IsFalse(letter.Medication.Contains(med));
        }

        private Medication Copy(Medication med)
        {
            return new Medication()
            {
                Date = med.Date,
                ID = med.ID,
                Name = med.Name,
                Profile = med.Profile,
                ProfileID = med.ProfileID,
                Sensitivity = med.Sensitivity,
                Dosis = med.Dosis,
                EndDate = med.EndDate,
                Frequency = med.Frequency, 
                Interval = med.Interval,
            };
        }
    }
}

using NUnit.Framework;
using myMD.Model.DataModel;
using myMDTests.Model.EntityFactory;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class MedicationTest
    {
        private Medication med;
        private RandomEntityFactory fac;

        [SetUp]
        public void SetUp()
        {
            fac = new RandomEntityFactory();
            med = fac.Medication();
        }

        [Test]
        public void SameEqualTest()
        {
            Assert.IsTrue(med.Equals(med));
        }

        [Test]
        public void CopiedEqualTest()
        {
            Assert.IsTrue(med.Equals(Copy(med)));
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

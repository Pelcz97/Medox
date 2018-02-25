using NUnit.Framework;
using myMD.Model.DataModel;
using myMDTests.Model.EntityFactory;
using myMD.ModelInterface.DataModelInterface;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class DoctorTest
    {
        private Doctor doc;
        private RandomEntityFactory fac;

        [SetUp] 
        public void SetUp()
        {
            fac = new RandomEntityFactory();
            doc = fac.Doctor();
        }

        [Test]
        public void SameEqualTest()
        {
            Assert.IsTrue(doc.Equals(doc));
            Assert.IsTrue(doc.Equals((object)doc));
        }

        [Test]
        public void IDoctorToDoctorEqualTest()
        {
            IDoctor iDoc = doc;
            Assert.IsTrue(doc.Equals(iDoc.ToDoctor()));
        }

        [Test]
        public void CopiedEqualTest()
        {
            Doctor copy = Copy(doc);
            Assert.IsTrue(doc.Equals(copy));
            Assert.AreEqual(doc.GetHashCode(), copy.GetHashCode());
        }

        [Test]
        public void NullEqualTest()
        {
            Assert.IsFalse(doc.Equals(null));
        }

        [Test]
        public void NotEqualTest()
        {
            Doctor changedDoc = Copy(doc);
            changedDoc.Field += "d";
            Assert.IsFalse(doc.Equals(changedDoc));
            changedDoc.Field = null;
            Assert.IsFalse(doc.Equals(changedDoc));
            changedDoc.Name = null;
            Assert.IsFalse(doc.Equals(changedDoc));
        }

        private Doctor Copy(Doctor doc) => new Doctor() { Field = doc.Field, ID = doc.ID, Name = doc.Name, Profile = doc.Profile, ProfileID = doc.ProfileID };
    }
}

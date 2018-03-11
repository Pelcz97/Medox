using NUnit.Framework;
using myMD.Model.DataModel;
using myMDTests.Model.EntityFactory;
using myMD.ModelInterface.DataModelInterface;
using System.Linq;
using myMDTests.Model.DependencyService;
using myMD.Model.DependencyService;
using myMD.Model.FileHelper;
using myMDTests.Model.FileHelper;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class DoctorsLetterTest
    {
        private static readonly int COUNT = 5;
        private static readonly string PATH = "letter.hl7";
        private DoctorsLetter letter;
        private RandomEntityFactory fac;
        private IDoctorsLetterGroup[] groups;
        private IMedication[] meds;
        private IDoctor doc;
        private static TestFileHelper helper;
        private string Path => helper.GetLocalFilePath(PATH);


        [OneTimeSetUp]
        public static void SetUpBefore()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
            helper = new TestFileHelper();
        }

        [SetUp]
        public void SetUp()
        {
            fac = new RandomEntityFactory();
            letter = fac.Letter();
            doc = fac.IDoctor();
            groups = new IDoctorsLetterGroup[COUNT];
            meds = new IMedication[COUNT];
            for (int i = 0; i < COUNT; ++i)
            {
                groups[i] = fac.IGroup();
                meds[i] = fac.IMedication();
            }
        }

        [Test]
        public void SameEqualTest()
        {
            Assert.IsTrue(letter.Equals(letter));
        }

        [Test]
        public void CopiedEqualTest()
        {
            object obj = Copy(letter);
            Assert.IsTrue(letter.Equals(obj));
            Assert.AreEqual(letter.GetHashCode(), obj.GetHashCode());
        }

        [Test]
        public void NullEqualTest()
        {
            Assert.IsFalse(letter.Equals(null));
        }

        [Test]
        public void NotEqualTest()
        {
            DoctorsLetter changedLetter = Copy(letter);
            changedLetter.Name += "d";
            Assert.IsFalse(letter.Equals(changedLetter));
        }

        [Test]
        public void DeleteTest()
        {
            foreach (IMedication med in meds)
            {
                letter.AttachMedication(med);
            }
            foreach (IDoctorsLetterGroup group in groups)
            {
                letter.AddToGroup(group);
            }
            letter.Filepath = Path;
            letter.Delete();
            Assert.IsFalse(letter.Medication.Any());
            Assert.IsFalse(letter.Groups.Any());
            Assert.IsFalse(helper.Exists(PATH));
        }

        [Test]
        public void MedicationTest()
        {
            foreach(IMedication med in meds)
            {
                letter.AttachMedication(med);
            }
            Assert.IsTrue(letter.Medication.SequenceEqual(meds));
            foreach (IMedication med in meds)
            {
                letter.DisattachMedication(med);
            }
            Assert.IsFalse(letter.Medication.Any());
        }

        [Test]
        public void GroupTest()
        {
            foreach (IDoctorsLetterGroup group in groups)
            {
                letter.AddToGroup(group);
            }
            Assert.IsTrue(letter.Groups.SequenceEqual(groups));
            foreach (IDoctorsLetterGroup group in groups)
            {
                letter.RemoveFromGroup(group);
            }
            Assert.IsFalse(letter.Groups.Any());
        }

        private DoctorsLetter Copy(DoctorsLetter letter)
        {
            return new DoctorsLetter()
            {
                Date = letter.Date,
                ID = letter.ID,
                Name = letter.Name,
                Profile = letter.Profile,
                ProfileID = letter.ProfileID,
                Diagnosis = letter.Diagnosis,
                Filepath = letter.Filepath,
                DoctorID = letter.DoctorID,
                Sensitivity = letter.Sensitivity
            };
        }
    }
}

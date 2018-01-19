using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using System;
using myMD.Model.DataModel;
using NUnit.Framework;
using System.IO;
using System.Linq;
using ModelInterface.DataModelInterface;
using myMDTests.Model.EntityFactory;

namespace myMDTests.Model.DatabaseModel
{
    //[TestFixture]
    public class DatabaseTest
    {
        private static readonly string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.db3");
        private static readonly int ENTITY_COUNT = 10;
        private static DoctorsLetter[] letters = new DoctorsLetter[ENTITY_COUNT];
        private static DoctorsLetterGroup[] groups = new DoctorsLetterGroup[ENTITY_COUNT];
        private static Medication[] meds = new Medication[ENTITY_COUNT];
        private static Profile[] profiles = new Profile[ENTITY_COUNT];
        private static Doctor[] doctors = new Doctor[ENTITY_COUNT];
        private static RandomEntityFactory factory;

        private static SQLiteConnection db;

        [OneTimeSetUp]
        public static void SetUpBefore()
        {
            System.Data.SQLite.SQLiteConnection.CreateFile(PATH);
            db = new SQLiteConnection(PATH);
            db.CreateTable<DoctorsLetter>();
            db.CreateTable<DoctorsLetterGroup>();
            db.CreateTable<Medication>();
            db.CreateTable<Profile>();
            db.CreateTable<Doctor>();
            db.CreateTable<DoctorsLetterGroupDoctorsLetter>();
            factory = new RandomEntityFactory();
            for (int i = 0; i < ENTITY_COUNT; ++i)
            {
                letters[i] = factory.Letter();
                groups[i] = factory.Group();
                meds[i] = factory.Medication();
                profiles[i] = factory.Profile();
                doctors[i] = factory.Doctor();
            }
            db.InsertAll(meds);
            db.InsertAll(profiles);
            db.InsertAll(doctors);
            db.InsertAll(groups);
            db.InsertAll(letters);
        }

        [SetUp]
        public void SetUp()
        {          
            
        }

        //[Test]
        public void MedLetterTest()
        {
            letters[0].AttachMedication(meds[0]);
            db.UpdateWithChildren(letters[0]);
            Medication med = db.GetWithChildren<Medication>(meds[0].ID, true);
            Assert.AreEqual(med.DatabaseDoctorsLetter.ID, meds[0].DatabaseDoctorsLetter.ID);
        }

        //[Test]
        public void GroupTest()
        {
            groups[0].Add(letters[0]);
            db.UpdateWithChildren(groups[0]);
            letters[0] = db.GetWithChildren<DoctorsLetter>(letters[0].ID, true);
            Assert.AreEqual(letters[0].DatabaseGroups.First().ID, db.Get<DoctorsLetterGroup>(groups[0].ID).ID);
        }

        [TearDown]
        public void TearDown()
        {
            //ClearArrays();
        }

        private void ClearArrays()
        {
            letters = new DoctorsLetter[ENTITY_COUNT];
            groups = new DoctorsLetterGroup[ENTITY_COUNT];
            meds = new Medication[ENTITY_COUNT];
            profiles = new Profile[ENTITY_COUNT];
            doctors = new Doctor[ENTITY_COUNT];
    }
    }
}

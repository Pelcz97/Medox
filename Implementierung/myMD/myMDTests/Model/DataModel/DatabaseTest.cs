using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using System;
using myMD.Model.DataModel;
using NUnit.Framework;
using System.IO;
using System.Linq;
using ModelInterface.DataModelInterface;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    class DatabaseTest
    {
        private static readonly string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.db3");
        private static readonly DateTime[] DATES = new DateTime[] {
            new DateTime(2017, 12, 24), new DateTime(2015, 1, 1), new DateTime(2018, 1, 1), new DateTime(1998, 8, 6), new DateTime(2000, 12, 24),
            new DateTime(2009, 1, 1), new DateTime(2011, 3, 1), new DateTime(1998, 8, 6), new DateTime(1998, 4, 6), new DateTime(2003, 9, 10),
            new DateTime(2009, 4, 24), new DateTime(2011, 9, 1), new DateTime(2018, 11, 1), new DateTime(1999, 10, 10), new DateTime(2007, 12, 24),
            new DateTime(2008, 12, 24), new DateTime(2001, 1, 1), new DateTime(2002, 1, 1), new DateTime(2013, 8, 6), new DateTime(2014, 12, 24),
            new DateTime(2009, 7, 30), new DateTime(2012, 5, 11), new DateTime(2011, 3, 3), new DateTime(2013, 4, 4), new DateTime(2014, 5, 5),
        };
        private DoctorsLetter[] letters = new DoctorsLetter[5];
        private DoctorsLetterGroup[] groups = new DoctorsLetterGroup[5];
        private Medication[] meds = new Medication[5];
        private Profile[] profiles = new Profile[5];
        private Doctor[] doctors = new Doctor[5];

        private SQLiteConnection db;

        [SetUp]
        public void SetUp()
        {
            System.Data.SQLite.SQLiteConnection.CreateFile(PATH);
            db = new SQLiteConnection(PATH);
            db.CreateTable<DoctorsLetter>();
            db.CreateTable<DoctorsLetterGroup>();
            db.CreateTable<Medication>();
            db.CreateTable<Profile>();
            db.CreateTable<Doctor>();
            db.CreateTable<DoctorsLetterGroupDoctorsLetter>();
            for (int i = 0; i < 5; ++i)
            {
                letters[i] = new DoctorsLetter { Date = DATES[5 * i] };
                groups[i] = new DoctorsLetterGroup { Date = DATES[5 * i + 1] };
                meds[i] = new Medication { Date = DATES[5 * i + 2], EndDate = DATES[5 * i + 3] };
                profiles[i] = new Profile();
                doctors[i] = new Doctor();
            }
            db.InsertAll(meds);
            db.InsertAll(profiles);
            db.InsertAll(doctors);
            db.InsertAll(groups);
            db.InsertAll(letters);
        }

        [Test]
        public void MedLetterTest()
        {
            letters[0].AttachMedication(meds[0]);
            db.UpdateWithChildren(letters[0]);
            Medication med = db.GetWithChildren<Medication>(meds[0].ID, true);
            Assert.AreEqual(med.DoctorsLetter.ID, meds[0].DoctorsLetter.ID);
        }

        [Test]
        public void GroupTest()
        {
            groups[0].Add(letters[0]);
            db.UpdateWithChildren(groups[0]);
            letters[0] = db.GetWithChildren<DoctorsLetter>(letters[0].ID, true);
            Assert.AreEqual(letters[0].DatabaseGroups.First().ID, db.Get<DoctorsLetterGroup>(groups[0].ID).ID);
        }

        [Test]
        public void Test()
        {
            IMedication med = meds[0];
            letters[0].AttachMedication(db.GetWithChildren<Medication>(med.ID, true));
            IDoctorsLetter letter = letters[0];
        }

        [TearDown]
        public void TearDown()
        {
            db = null;
            letters = null;
            groups = null;
            profiles = null;
            doctors = null;
        }
    }
}

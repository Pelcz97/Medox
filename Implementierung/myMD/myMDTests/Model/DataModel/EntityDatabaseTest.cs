using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMD.Model.DatabaseModel;
using NUnit.Framework;
using System.IO;
using myMD.Model.DataModel;
using ModelInterface.DataModelInterface;

namespace myMDTests.Model.DataModel
{
    [TestFixture]
    public class EntityDatabaseTest
    {
        private static EntityDatabase db;
        private static readonly string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.db3");
        private static readonly int ENTITY_COUNT = 10;
        private static DoctorsLetter[] letters = new DoctorsLetter[ENTITY_COUNT];
        private static DoctorsLetterGroup[] groups = new DoctorsLetterGroup[ENTITY_COUNT];
        private static Medication[] meds = new Medication[ENTITY_COUNT];
        private static Profile[] profiles = new Profile[ENTITY_COUNT];
        private static Doctor[] doctors = new Doctor[ENTITY_COUNT];
        private static RandomEntityFactory factory;

        [OneTimeSetUp]
        public void SetUpBefore()
        {
            db = new EntityDatabase(PATH);
            factory = new RandomEntityFactory();
            for (int i = 0; i < ENTITY_COUNT; ++i)
            {
                letters[i] = factory.Letter();
                db.Insert(letters[i]);
                groups[i] = factory.Group();
                db.Insert(groups[i]);
                meds[i] = factory.Medication();
                db.Insert(meds[i]);
                profiles[i] = factory.Profile();
                db.Insert(profiles[i]);
                doctors[i] = factory.Doctor();
                db.Insert(doctors[i]);
                doctors[i].Profile = profiles[i];
                db.Update(doctors[i]);
            }
        }

        [Test]
        public void GetDataFromProfileTest()
        {
            doctors[0].Profile = profiles[0];
            doctors[1].Profile = profiles[0];
            db.Activate(profiles[0]);
            db.Update(doctors[0]);
            db.Update(doctors[1]);
            IList<Doctor> docs = db.GetAllDataFromProfile<Doctor>();
            Assert.True(docs.First().ID == doctors[0].ID && docs.Last().ID == doctors[1].ID && docs.Count == 2);
        }

        [OneTimeTearDown]
        public static void TearDownAfter()
        {
            db.Destroy();
        }

    }
}

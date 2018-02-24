using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMD.Model.DatabaseModel;
using NUnit.Framework;
using System.IO;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using myMDTests.Model.EntityFactory;
using myMDTests.Model.FileHelper;
using myMD.Model.DependencyService;
using myMDTests.Model.DependencyService;

namespace myMDTests.Model.DatabaseModel
{
    [TestFixture]
    public class EntityDatabaseTest
    {
        private static EntityDatabase db;
        private static readonly int ENTITY_COUNT = 100;
        private static DoctorsLetter[] letters = new DoctorsLetter[ENTITY_COUNT];
        private static DoctorsLetterGroup[] groups = new DoctorsLetterGroup[ENTITY_COUNT];
        private static Medication[] meds = new Medication[ENTITY_COUNT];
        private static Profile[] profiles = new Profile[ENTITY_COUNT];
        private static Doctor[] doctors = new Doctor[ENTITY_COUNT];
        private static RandomEntityFactory factory;

        [OneTimeSetUp]
        public void SetUpBefore()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
            db = new EntityDatabase();
            factory = new RandomEntityFactory();
            db.Destroy();
            db.Create();
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
                groups[i].Profile = profiles[i];
                db.Update(groups[i]);
                letters[i].Profile = profiles[i];
                letters[i].DatabaseDoctor = doctors[i];
                db.Update(letters[i]);
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

        [Test]
        public void DoctorsLetterGroupDoctorsLetterTest()
        {
            groups[0].Add(letters[0]);
            db.Activate(profiles[0]);
            db.Update(groups[0]);
            IList<DoctorsLetter> letter = db.GetAllDataFromProfile<DoctorsLetter>();
            var g = letter.First().DatabaseGroups.First();
            db.Update(letters[0]);
            Assert.True(g.Equals(groups[0]));    
            Assert.True(g.Equals(db.GetAllDataFromProfile<DoctorsLetterGroup>().First()));
            Assert.True(g.Equals(db.GetAllDataFromProfile<DoctorsLetterGroup>().First()));
            Assert.True(db.GetAllDataFromProfile<DoctorsLetter>().First().DatabaseGroups.First().Equals(db.GetAllDataFromProfile<DoctorsLetterGroup>().First()));
        }

        [Test]
        public void EqualTest()
        {
            Assert.True(groups[0].Equals(groups[0]));
        }
    }
}

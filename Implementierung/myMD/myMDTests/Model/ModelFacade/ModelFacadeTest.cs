using myMD.ModelInterface.ModelFacadeInterface;
using myMD.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMD.Model.DatabaseModel;
using myMD.Model.TransmissionModel;
using myMD.Model.EntityFactory;
using myMD.Model.ParserModel;
using myMDTests.Model.DependencyService;
using myMD.Model.DependencyService;
using myMD.ModelInterface.DataModelInterface;
using myMDTests.Model.TransmissionModel;
using myMDTests.Model.EntityFactory;
using myMD.Model.DataModel;

namespace myMDTests.Model.ModelFacade
{
    [TestFixture]
    public class ModelFacadeTest
    {
        private IModelFacade model;
        private BluetoothStub bluetooth;
        private EntityDatabase database;
        private IEntityFactory factory;
        private RandomEntityFactory randFac;
        private IParserFacade parser;
        private IDependencyService service;

        [OneTimeSetUp]
        public void SetUpBefore()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
            bluetooth = new BluetoothStub();
            database = new EntityDatabase();
            factory = new myMD.Model.EntityFactory.EntityFactory();
            parser = new ParserFacade();
            service = new TestDependencyService();
            database.Destroy();
            database.Create();
            randFac = new RandomEntityFactory();
        }

        [SetUp]
        public void SetUp()
        {              
            model = new myMD.Model.ModelFacade.ModelFacade(database, factory, parser, bluetooth);
        }

        [Test]
        public void ProfileTest()
        {
            IProfile profile = model.CreateEmptyProfile();
            model.Activate(profile);
            Assert.AreEqual(profile, model.GetActiveProfile());
        }

        [Test]
        public void MedicationTest()
        {
            IMedication med = model.CreateEmptyMedication();
            IList<IMedication> meds = model.GetAllMedications();
            Assert.IsTrue(meds.Contains(med));
        }

        [Test]
        public void GroupTest()
        {
            IDoctorsLetterGroup group = model.CreateEmptyGroup();
            Assert.IsTrue(model.GetAllGroups().Contains(group));
        }

        [Test]
        public void UpdateTest()
        {
            IDoctorsLetterGroup group = model.CreateEmptyGroup();
            database.Insert(factory.CreateEmptyDoctorsLetter());
            foreach (IDoctorsLetter letter in model.GetAllDoctorsLetters())
            {
                group.Add(letter);
            }
            model.Update(group);
            IList<IDoctorsLetterGroup> groups = model.GetAllGroups();
            Assert.IsTrue(groups.Contains(group));
        }

        [Test]
        public void SendTest()
        {
            DoctorsLetter letter = randFac.Letter();
            model.SendLetter(letter);
            Assert.AreEqual(letter.Filepath, bluetooth.File);
        }

        [Test]
        public void DeleteTest()
        {
            IMedication med = model.CreateEmptyMedication();
            Assert.Contains(med, model.GetAllMedications().ToList());
            model.Delete(med);
            Assert.IsFalse(model.GetAllMedications().Contains(med));
        }
    }
}

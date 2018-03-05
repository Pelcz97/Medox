using myMD.Model.DataModel;
using myMD.Model.DependencyService;
using myMD.Model.FileHelper;
using myMD.Model.ParserModel;
using myMDTests.Model.DependencyService;
using myMDTests.Model.EntityFactory;
using myMDTests.Model.FileHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMDTests.Model.ParserModel
{
    [TestFixture]
    public class ParserFacadeTest
    {
        private static readonly string FAIL = "fail.fail";

        private static string Fail => FAIL;

        private IParserFacade parser;

        private RandomEntityFactory fac;

        private DoctorsLetter letter;

        private EntityDatabaseStub db;

        [OneTimeSetUp]
        public static void SetUpBefore()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
        }

        [SetUp]
        public void SetUp()
        {
            parser = new ParserFacade();
            fac = new RandomEntityFactory();
            letter = fac.Letter();
            db = new EntityDatabaseStub();
        }

        [Test]
        public void ParseLetterToOriginalFileTest()
        {
            Assert.AreEqual(parser.ParseLetterToOriginalFile(letter), letter.Filepath);
        }

        [Test]
        public void ParseCustomFileToDatabaseTest()
        {
            parser.ParseFileToDatabase(Hl7FileUtility.Custom(0), db);
            Assert.AreEqual(db.Doctor, Hl7FileUtility.Doctor);
            Assert.AreEqual(db.Doctor.Profile, db.Profile);
            Assert.AreEqual(db.Letter, Hl7FileUtility.Letter[0]);
            Assert.AreEqual(db.Letter.Profile, db.Profile);
            Assert.IsTrue(db.Meds.SequenceEqual(Hl7FileUtility.Meds));
            Assert.IsTrue(db.Meds.SequenceEqual(db.Letter.Medication));
            foreach (Medication med in db.Meds)
            {
                Assert.AreEqual(med.Profile, db.Profile);
                Assert.AreEqual(med.DoctorsLetter, db.Letter);
            }
            Assert.AreEqual(db.Letter.Doctor, db.Doctor);
        }

        [Test]
        public void TryParseInvalidToDatabaseTest()
        {
            Assert.Throws<NotSupportedException>(() => parser.ParseFileToDatabase(Fail, db));
        }
    }
}

using myMD.Model.DataModel;
using NUnit.Framework;
using System.Linq;

namespace myMDTests.Model.ParserModel
{
    [TestFixture]
    public class FileToDatabaseParserTest
    {
        private MockFileToDatabaseParser parser;

        private EntityDatabaseStub db;

        [SetUp]
        public void SetUp()
        {
            db = new EntityDatabaseStub();
            parser = new MockFileToDatabaseParser();
        }

        [Test]
        public void ParseFileTest()
        {
            parser.ParseFile("", db);
            Assert.AreEqual(db.Doctor, parser.Doctor);
            Assert.AreEqual(db.Doctor.Profile, db.Profile);
            Assert.AreEqual(db.Letter, parser.Letter);
            Assert.AreEqual(db.Letter.Profile, db.Profile);
            Assert.IsTrue(db.Meds.SequenceEqual(parser.Meds));
            Assert.IsTrue(db.Meds.SequenceEqual(db.Letter.Medication));
            foreach (Medication med in db.Meds)
            {
                Assert.AreEqual(med.Profile, db.Profile);
                Assert.AreEqual(med.DoctorsLetter, db.Letter);
            }
            Assert.AreEqual(db.Letter.Doctor, db.Doctor);
        }
    }
}
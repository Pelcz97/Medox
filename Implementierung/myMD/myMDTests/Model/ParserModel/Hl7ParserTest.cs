using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.Xml;
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
using System.Xml;

namespace myMDTests.Model.ParserModel
{
    [TestFixture]
    public class Hl7ParserTest
    {
        private static readonly string SAMPLE = "test.hl7";

        private static readonly string FAIL = "fail.fail";

        private static readonly string HL7_FAIL = "fail.hl7";

        private static readonly RandomEntityFactory fac = new RandomEntityFactory();

        private static IFileHelper helper = new TestFileHelper();

        private static IParserFacade parser = new ParserFacade();

        private static string Sample => helper.GetLocalFilePath(SAMPLE);

        private static string Fail => FAIL;

        private static string Hl7Fail => helper.GetLocalFilePath(HL7_FAIL);

        private static EntityDatabaseStub db = new EntityDatabaseStub();

        [OneTimeSetUp]
        public static void SetUp()
        {
            DependencyServiceWrapper.Service = new TestDependencyService();
        }

        [Test]
        public void ParseSampleTest()
        {
            IFormatterParseResult parseResult;
            XmlIts1Formatter fmtr = new XmlIts1Formatter()
            {
                ValidateConformance = false
            };
            fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
            using (var xr = new XmlStateReader(XmlReader.Create(Sample)))
            {
                parseResult = fmtr.Parse(xr, typeof(ClinicalDocument));
            }
            ClinicalDocument document = parseResult.Structure as ClinicalDocument;
            Assert.NotNull(document);
        }

        [Test]
        public void ParseFromDocumentTest()
        {
            var parser = new TestHl7ToDatabaseParser();
            parser.Init(Hl7FileUtility.Custom(0));
            DoctorsLetter parsedLetter = parser.ParseLetter();
            Doctor parsedDoctor = parser.ParseDoctor();
            Profile parsedProfile = parser.ParseProfile();
            IList<Medication> parsedMeds = parser.ParseMedications();
            foreach (Medication med in parsedMeds)
            {
                parsedLetter.AttachMedication(med);
                med.Profile = Hl7FileUtility.Profile;
            }
            parsedLetter.DatabaseDoctor = parsedDoctor;
            parsedLetter.Profile = Hl7FileUtility.Profile;
            parsedDoctor.Profile = Hl7FileUtility.Profile;
            Assert.AreEqual(parsedDoctor, Hl7FileUtility.Doctor);
            Assert.AreEqual(parsedLetter, Hl7FileUtility.Letter[0]);
            Assert.AreEqual(parsedProfile, Hl7FileUtility.Profile);
            Assert.IsTrue(parsedMeds.SequenceEqual(Hl7FileUtility.Meds));
        }

        [Test]
        public void ParseHl7FileToDatabaseTest()
        {
            FileToDatabaseParser parser = new Hl7ToDatabaseParser();
            parser.ParseFile(Hl7FileUtility.Custom(0), db);
            Assert.AreEqual(db.Doctor, Hl7FileUtility.Doctor);
            Assert.AreEqual(db.Letter, Hl7FileUtility.Letter[0]);
            Assert.AreEqual(db.Profile, Hl7FileUtility.Profile);
            Assert.IsTrue(db.Meds.SequenceEqual(Hl7FileUtility.Meds));
        }

        [Test]
        public void ParseLetterToFileTest()
        {
            Assert.AreEqual(parser.ParseLetterToOriginalFile(Hl7FileUtility.Letter[0]), Hl7FileUtility.Custom(0));
        }

        [Test]
        public void UnsupportedFileFormatTest()
        {
            Assert.Throws<NotSupportedException>(() => parser.ParseFileToDatabase(Fail, db));
        }

        [Test]
        public void InvalidFileTest()
        {
            Assert.Throws<FileFormatException>(() => parser.ParseFileToDatabase(Hl7Fail, db));
        }
    }
}
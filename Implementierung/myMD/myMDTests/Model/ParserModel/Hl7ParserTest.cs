using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.Xml;
using myMD.Model.DataModel;
using myMD.Model.FileHelper;
using myMD.Model.ParserModel;
using myMDTests.Model.EntityFactory;
using myMDTests.Model.FileHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace myMDTests.Model.ParserModel
{
    [TestFixture]
    public class Hl7ParserTest
    {
        private static readonly string SAMPLE = "test.hl7";

        private static readonly string CUSTOM = "custom.hl7";

        private static readonly string FAIL = "fail.fail";

        private static readonly RandomEntityFactory fac = new RandomEntityFactory();

        private static Doctor doctor;

        private static Profile profile;

        private static DoctorsLetter letter;

        private static Medication[] meds;

        private static IFileHelper helper = new TestFileHelper();

        private static IParserFacade parser = new ParserFacade();

        private static string Custom => helper.GetLocalFilePath(CUSTOM);

        private static string Sample => helper.GetLocalFilePath(SAMPLE);

        private static string Fail => FAIL;

        private static EntityDatabaseStub db = new EntityDatabaseStub();

        [OneTimeSetUp]
        public static void SetUp()
        {
            doctor = new Doctor
            {
                Name = "Dr. Jan Itor",
                Field = "Experte auf diesem Gebiet"
            };
            profile = new Profile
            {
                Name = "Philipp",
                LastName = "Karcher",
                BirthDate = new DateTime(1998, 8, 6),
                InsuranceNumber = "123456",
            };
            letter = new DoctorsLetter
            {
                Name = "Arztbesuch",
                Date = DateTime.Today,
                Diagnosis = "Diagnose",
                Sensitivity = myMD.ModelInterface.DataModelInterface.Sensitivity.High,
                Filepath = Custom,
        };
            meds = new Medication[]{
                new Medication
                {
                    Name = "Heroin",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(14),
                    Dosis = "500mg",
                    Frequency = 5,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Week,
                    Sensitivity = letter.Sensitivity,
                },
                new Medication
                {
                    Name = "Aspirin",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(7),
                    Dosis = "4 Schachteln",
                    Frequency = 1,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Hour,
                    Sensitivity = letter.Sensitivity,
                },
                new Medication
                {
                    Name = "Ibuprofen",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(100),
                    Dosis = "3mg",
                    Frequency = 8,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Day,
                    Sensitivity = letter.Sensitivity,
                },
            };
            foreach (Medication med in meds)
            {
                letter.AttachMedication(med);
            }
            letter.Profile = profile;
            letter.DatabaseDoctor = doctor;
            CreateDocument();
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
            parser.Init(Custom);
            DoctorsLetter parsedLetter = parser.ParseLetter();
            Doctor parsedDoctor = parser.ParseDoctor();
            Profile parsedProfile = parser.ParseProfile();
            IList<Medication> parsedMeds = parser.ParseMedications();
            foreach (Medication med in parsedMeds)
            {
                parsedLetter.AttachMedication(med);
            }
            parsedLetter.DatabaseDoctor = parsedDoctor;
            parsedLetter.Profile = parsedProfile;
            Assert.AreEqual(parsedDoctor, doctor);
            Assert.AreEqual(parsedLetter, letter);
            Assert.AreEqual(parsedProfile, profile);
            Assert.IsTrue(parsedMeds.SequenceEqual(meds));
        }

        [Test]
        public void ParseHl7FileToDatabaseTest()
        {
            FileToDatabaseParser parser = new Hl7ToDatabaseParser();
            parser.ParseFile(Custom, db);
            Assert.AreEqual(db.Doctor, doctor);
            Assert.AreEqual(db.Letter, letter);
            Assert.AreEqual(db.Profile, profile);
            Assert.IsTrue(db.Meds.SequenceEqual(meds));
        }

        [Test]
        public void ParseLetterToFileTest()
        {      
            Assert.AreEqual(parser.ParseLetterToOriginalFile(letter), Custom);
        }

        [Test]
        public void UnsupportedFileFormatTest()
        {
            Assert.Throws<NotSupportedException>(() => parser.ParseFileToDatabase(Fail, db));
        }

        private static void CreateDocument()
        {
            using (XmlIts1Formatter fmtr = new XmlIts1Formatter())
            {
                fmtr.Settings = SettingsType.DefaultUniprocessor;
                // We want to use CDA data types
                using (ClinicalDocumentDatatypeFormatter dtfmtr = new ClinicalDocumentDatatypeFormatter())
                {
                    // This is a good idea to prevent validation errors
                    fmtr.ValidateConformance = false;
                    // This instructs the XML ITS1 Formatter we want to use CDA datatypes
                    fmtr.GraphAides.Add(dtfmtr);
                    // Output in a nice indented manner
                    using (XmlWriter xw = XmlWriter.Create(helper.GetLocalFilePath(CUSTOM), new XmlWriterSettings() { Indent = true }))
                    {
                        fmtr.Graph(xw, CreateCDA(profile, doctor, letter));
                    }
                }
            }
        }

        private static ClinicalDocument CreateCDA(Profile profile, Doctor doctor, DoctorsLetter letter)
        {
            // First, we need to create a clinical document
            ClinicalDocument doc = new ClinicalDocument()
            {
                TypeId = new II("2.16.840.1.113883.1.3", "POCD_HD000040"), // This value is static and identifies the HL7 type
                RealmCode = SET<CS<BindingRealm>>.CreateSET(BindingRealm.UniversalRealmOrContextUsedInEveryInstance), // This is UV some profiles require US
                EffectiveTime = new TS(letter.Date),
                ConfidentialityCode = (x_BasicConfidentialityKind)letter.Sensitivity,
                LanguageCode = "en-US", // Language of the CDA
            };
            RecordTarget patient = new RecordTarget
            {
                ContextControlCode = ContextControl.OverridingPropagating,
                PatientRole = new PatientRole()
                {
                    Id = new SET<II>(),
                    Patient = new Patient()
                    {
                        Name = SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, profile.LastName, profile.Name)), // PAtient name
                        BirthTime = new TS(profile.BirthDate, DatePrecision.Day) // Day of birth
                    }
                }
            };
            patient.PatientRole.Patient.Id = new II("1.2.3.4.5.6", profile.InsuranceNumber);
            doc.RecordTarget.Add(patient);
            Author docAuthor = new Author()
            {
                ContextControlCode = ContextControl.AdditivePropagating,
                Time = letter.Date, // When the document was created
                AssignedAuthor = new AssignedAuthor()
                {
                    Id = SET<II>.CreateSET(new II("1.2.3.4.5.6.1", "")), // Physician's identifiers (or how we know the physician)
                    AssignedAuthorChoice = AuthorChoice.CreatePerson(
                    SET<PN>.CreateSET(PN.FromFamilyGiven(EntityNameUse.Legal, doctor.Name), PN.FromFamilyGiven(EntityNameUse.Assigned, doctor.Field)) // The author's name
                    ),
                },
            };
            doc.Author.Add(docAuthor);
            doc.Component = new Component2(ActRelationshipHasComponent.HasComponent, true, new StructuredBody());
            Section section = new Section
            {
                Text = new ED
                {
                    Data = Encoding.ASCII.GetBytes(letter.Diagnosis)
                },
                Title = letter.Name,
                ConfidentialityCode = (x_BasicConfidentialityKind)letter.Sensitivity,
                Entry = new List<Entry>()
            };
            foreach (Medication med in letter.DatabaseMedication)
            {
                SubstanceAdministration substance = new SubstanceAdministration();
                int index = med.Dosis.LastIndexOfAny("0123456789".ToCharArray()) + 1;
                substance.DoseQuantity = new IVL<PQ>(new PQ(Decimal.Parse(med.Dosis.Substring(0, index)), med.Dosis.Substring(index)));
                substance.Consumable = new Consumable(new ManufacturedProduct(new CS<RoleClassManufacturedProduct>(), new LabeledDrug { Name = new EN(EntityNameUse.Legal, new List<ENXP> { new ENXP(med.Name) }) }));
                substance.EffectiveTime = new List<GTS> { new GTS(new PIVL<TS> {
                    Phase = new IVL<TS>(med.Date, med.EndDate),
                    Frequency = new RTO<INT, PQ>(new INT(med.Frequency), new PQ(1, ((char) med.Interval).ToString())),
                }) };
                section.Entry.Add(new Entry(x_ActRelationshipEntry.DRIV, new BL(true), substance));
            }
            doc.Component.GetBodyChoiceIfStructuredBody().Component.Add(new Component3(ActRelationshipHasComponent.HasComponent, true, section));
            return doc;
        }
    }
}
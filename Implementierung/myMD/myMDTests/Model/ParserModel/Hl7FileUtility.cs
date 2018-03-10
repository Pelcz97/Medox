using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using myMD.Model.DataModel;
using myMD.Model.FileHelper;
using myMD.ModelInterface.DataModelInterface;
using myMDTests.Model.FileHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace myMDTests.Model.ParserModel
{
    public static class Hl7FileUtility
    {
        private static readonly string[] CUSTOM = { "custom.hl7", "custom2.hl7", "custom3.hl7", "custom4.hl7", "custom5.hl7", };

        private static readonly int COUNT = 5;

        public static Doctor Doctor { get; }

        public static Profile Profile { get; }

        public static DoctorsLetter[] Letter { get; }

        public static Medication[] Meds { get; }

        private static IFileHelper helper = new TestFileHelper();

        public static string Custom(int i) => helper.GetLocalFilePath(CUSTOM[i]);

        static Hl7FileUtility()
        {
            Doctor = new Doctor
            {
                Name = "Dr. Jan Itor",
                Field = "Experte auf diesem Gebiet"
            };
            Profile = new Profile
            {
                Name = "Philipp",
                LastName = "Karcher",
                BirthDate = new DateTime(1998, 8, 6),
                InsuranceNumber = "123456",
            };
            Letter = new DoctorsLetter[] {
                new DoctorsLetter
                {
                    Name = "Arztbesuch",
                    Date = DateTime.Today,
                    Diagnosis = "Diagnose",
                    Sensitivity = myMD.ModelInterface.DataModelInterface.Sensitivity.High,
                    Filepath = Custom(0),
                },
                new DoctorsLetter
                {
                    Name = "Armbruch",
                    Date = DateTime.Today,
                    Diagnosis = "Arm gebrochen",
                    Sensitivity = myMD.ModelInterface.DataModelInterface.Sensitivity.High,
                    Filepath = Custom(1),
                },
                new DoctorsLetter
                {
                    Name = "Routineuntersuchung",
                    Date = DateTime.Today,
                    Diagnosis = "Alles okay",
                    Sensitivity = myMD.ModelInterface.DataModelInterface.Sensitivity.Low,
                    Filepath = Custom(2),
                },
                new DoctorsLetter
                {
                    Name = "Beinbruch",
                    Date = DateTime.Today,
                    Diagnosis = "Bein gebrochen",
                    Sensitivity = myMD.ModelInterface.DataModelInterface.Sensitivity.High,
                    Filepath = Custom(3),
                },
                new DoctorsLetter
                {
                    Name = "Wundkontrolle",
                    Date = DateTime.Today,
                    Diagnosis = "Alles okay",
                    Sensitivity = myMD.ModelInterface.DataModelInterface.Sensitivity.Normal,
                    Filepath = Custom(4),
                },
            };
            Meds = new Medication[]{
                new Medication
                {
                    Name = "Heroin",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(14),
                    Dosis = "500mg",
                    Frequency = 5,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Week,
                    Sensitivity = Letter[0].Sensitivity,
                },
                new Medication
                {
                    Name = "Aspirin",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(7),
                    Dosis = "4 Schachteln",
                    Frequency = 1,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Hour,
                    Sensitivity = Letter[0].Sensitivity,
                },
                new Medication
                {
                    Name = "Ibuprofen",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(100),
                    Dosis = "3mg",
                    Frequency = 8,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Day,
                    Sensitivity = Letter[0].Sensitivity,
                },
                new Medication
                {
                    Name = "Paracetamol",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(7),
                    Dosis = "100mg",
                    Frequency = 5,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Week,
                    Sensitivity = Letter[0].Sensitivity,
                },
                new Medication
                {
                    Name = "Diclofenac",
                    Date = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(100),
                    Dosis = "50mg",
                    Frequency = 2,
                    Interval = myMD.ModelInterface.DataModelInterface.Interval.Day,
                    Sensitivity = Letter[0].Sensitivity,
                },
            };
            int i = 0;
            foreach (Medication med in Meds)
            {
                Letter[GetLetter(i)].AttachMedication(med);
                med.Profile = Profile;
                ++i;
            }
            Doctor.Profile = Profile;
            Letter[0].Profile = Profile;
            Letter[0].DatabaseDoctor = Doctor;
            CreateDocuments();
        }

        private static int GetLetter(int i)
        {
            if (i < 3) return 0;
            else if (i < 4) return 1;
            else return 3;
        }

        public static void CreateDocuments()
        {
            ClinicalDocument p = new ClinicalDocument();
            p.Validate();
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
                    for (int i = 0; i < COUNT; ++i)
                    {
                        var letters = Letter;
                        var letter = letters[i];
                        using (XmlWriter xw = XmlWriter.Create(Custom(i), new XmlWriterSettings() { Indent = true }))
                        {
                            fmtr.Graph(xw, CreateCDA(Profile, Doctor, Letter[0]));
                        }
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
                ConfidentialityCode = ParseSensitivity(letter.Sensitivity),
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
                        BirthTime = new TS(profile.BirthDate, DatePrecision.Full) // Day of birth
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
                ConfidentialityCode = ParseSensitivity(letter.Sensitivity),
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

        private static x_BasicConfidentialityKind ParseSensitivity(Sensitivity sensitvity)
        {
            switch(sensitvity)
            {
                case Sensitivity.High: return x_BasicConfidentialityKind.VeryRestricted;
                case Sensitivity.Low: return x_BasicConfidentialityKind.Normal;
                case Sensitivity.Normal: return x_BasicConfidentialityKind.Restricted;
                default: return default(x_BasicConfidentialityKind);
            }
        }
    }
}
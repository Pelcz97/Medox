using MARC.Everest.Connectors;
using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.RMIM.UV.CDAr2.Vocabulary;
using MARC.Everest.RMIM.UV.NE2010.RIM;
using MARC.Everest.Xml;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse ist ein FileToDatabaseParser für Dateien im .hl7 Format.
    /// Das tatsächliche Parsen der Datei wird dabei größtenteils an das Everest Framework delegiert.
    /// </summary>
    /// <see>myMD.Model.ParserModel.FileToDatabaseParser</see>
	public class Hl7ToDatabaseParser : FileToDatabaseParser
    {
        /// <summary>
        /// Das Dokument aus dem beim Aufrufen der Parse-Methoden gelesen wird.
        /// Muss zuvor durch Aufruf der Init() Methode initialisiert werden.
        /// </summary>
        private ClinicalDocument document;

        /// <summary>
        /// Der Dateipfad zu dem Dokument aus dem gerade gelesen wird.
        /// </summary>
        private string file;

        /// <summary>
        /// Helfer zum ausführen plattformsepzifischer Operationen
        /// </summary>
        private IHl7ParserHelper helper;

        /// <summary>
        /// Konstruktor mit automatisch nach Plattform ausgewähltem Helfer.
        /// </summary>
        public Hl7ToDatabaseParser() : this(Xamarin.Forms.DependencyService.Get<IHl7ParserHelper>()) { }

        /// <summary>
        /// Konstrukter mit Helfer als Parameter.
        /// </summary>
        /// <param name="helper">Der Helfer der zum Ausführen plattformspezifischer Operationen verwendet werden soll</param>
        public Hl7ToDatabaseParser(IHl7ParserHelper helper) => this.helper = helper;

        /// <summary>
        /// Initialisiert document, in dem es aus dem gegebenen Dateipfad liest.
        /// </summary>
        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#Init(string)</see>
        protected override void Init(string file)
        {
            this.file = file;
            XmlStateReader xr = new XmlStateReader(XmlReader.Create(file));
            XmlIts1Formatter fmtr = new XmlIts1Formatter()
            {
                ValidateConformance = false
            };
            helper.PrepareFormatter(fmtr);
            IFormatterParseResult parseResult = fmtr.Parse(new XmlStateReader(XmlReader.Create(file)), typeof(ClinicalDocument));
            document = parseResult.Structure as ClinicalDocument;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override Doctor ParseDoctor()
        {
            //Der Arzt ist der Autor des Dokuments
            Person author = document.Author.First().AssignedAuthor.AssignedAuthorChoice as Person;
            return new Doctor
            {
                //Suche den legalen Namen des Arztes
                Name = author.Name.Find(v => v.Use.Items.Any(w => w.Code.Equals(EntityNameUse.Legal))).ToString(),
                //Suche zugeordneten Namen des Arztes
                Field = author.Name.Find(v => v.Use.Items.Any(w => w.Code.Equals(EntityNameUse.Assigned))).ToString(),
            };
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override DoctorsLetter ParseLetter()
        {
            //Suche Hauptsektion des Dokuments
            Section letter = document.Component.GetBodyChoiceIfStructuredBody().Component.First().Section;
            x_BasicConfidentialityKind sensitivity = letter.ConfidentialityCode.Code;
            return new DoctorsLetter
            {
                Filepath = file,
                Name = letter.Title,
                Diagnosis = System.Text.Encoding.UTF8.GetString(letter.Text.Data, 0, letter.Text.Data.Length),
                Date = document.EffectiveTime.DateValue,
                Sensitivity = (Sensitivity)sensitivity,
            };
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override IList<Medication> ParseMedications()
        {
            IList<Medication> meds = new List<Medication>();
            //Suche nach Einträgen über Medikationen
            List<Entry> entries = document.Component.GetBodyChoiceIfStructuredBody().Component.First().Section.Entry.FindAll(v => v.ClinicalStatement.IsPOCD_MT000040UVSubstanceAdministration());
            x_BasicConfidentialityKind sensitivity = document.Component.GetBodyChoiceIfStructuredBody().Component.First().Section.ConfidentialityCode.Code;
            foreach (Entry entry in entries)
            {
                //Parse jeden dieser Einträge in eine Medikation
                meds.Add(ParseMedication(entry.GetClinicalStatementIfSubstanceAdministration(), (Sensitivity) sensitivity));
            }
            return meds;
        }

        /// <summary>
        /// Parst die gegebenen Informationen in eine Medikation
        /// </summary>
        /// <param name="med">Die zu parsenden Informationen über die Medikation</param>
        /// <param name="sensitivity">Die Sensitivitätsstufe der Medikation</param>
        /// <returns>Medikation mit den Informationen aus med</returns>
        private Medication ParseMedication(SubstanceAdministration med, Sensitivity sensitivity)
        {
            Medication target = new Medication()
            {
                Dosis = String.Concat(med.DoseQuantity.Value.ToInt().ToString(), med.DoseQuantity.Value.Unit),
                Name = med.Consumable.ManufacturedProduct.GetManufacturedDrugOrOtherMaterialIfManufacturedLabeledDrug().Name.Part.First(),
                Sensitivity = sensitivity,
            };
            //Führe plattformspezifische Operationen aus
            helper.FinalizeMedication(med, target);
            return target;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override Profile ParseProfile()
        {
            //Die Informationen der Profile Klasse sind Informationen über einen Patient.
            Patient patient = document.RecordTarget.First().PatientRole.Patient;
            //Suche nach dem zusammengesetzten legalen Namen
            List<ENXP> part = patient.Name.Find(v => v.Use.Items.Any(w => w.Code.Equals(EntityNameUse.Legal))).Part;
            return new Profile
            {
                BirthDate = patient.BirthTime.DateValue.Date,
                //Suche den Familiennamen
                Name = part.Find(v => v.Type.Equals(EntityNamePartType.Given)).Value,
                //Suche den Vornamen
                LastName = part.Find(v => v.Type.Equals(EntityNamePartType.Family)).Value,
                InsuranceNumber = patient.Id.Extension,
            };
        }
    }
}
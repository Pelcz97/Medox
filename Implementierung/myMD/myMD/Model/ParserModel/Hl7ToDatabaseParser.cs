using myMD.Model.DataModel;
using myMD.Model.DependencyService;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse ist ein FileToDatabaseParser für Dateien im .hl7 Format.
    /// Das tatsächliche Parsen der Datei wird dabei größtenteils an das Everest Framework delegiert.
    /// </summary>
    /// <see>myMD.Model.ParserModel.FileToDatabaseParser</see>
	public class Hl7ToDatabaseParser : FileToDatabaseParser
    {

        private static readonly string INVALID_FILE_MESSAGE = "Invalid File";

        /// <summary>
        /// Das Dokument aus dem beim Aufrufen der Parse-Methoden gelesen wird.
        /// Muss zuvor durch Aufruf der Init() Methode initialisiert werden.
        /// </summary>
        private XDocument document;

        /// <summary>
        /// Der Dateipfad zu dem Dokument aus dem gerade gelesen wird.
        /// </summary>
        private string file;

        /// <summary>
        /// Initialisiert document, in dem es aus dem gegebenen Dateipfad liest.
        /// </summary>
        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#Init(string)</see>
        protected override void Init(string file)
        {
            this.file = file;
            try
            {
                document = XDocument.Load(file);
            } catch (XmlException e)
            {
                throw new FileFormatException(INVALID_FILE_MESSAGE, e);
            }
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override Doctor ParseDoctor()
        {
            //Der Arzt ist der Autor des Dokuments
            IEnumerable<XElement> names = SingleByName(document.Descendants(), "assignedPerson").Elements();
            return new Doctor
            {
                //Suche den legalen Namen des Arztes
                Name = SingleByName(SingleByAttributeName(names, "use", "L").Elements(), "family").Value,
                //Suche zugeordneten Namen des Arztes
                Field = SingleByName(SingleByAttributeName(names, "use", "ASGN").Elements(), "family").Value,
            };
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override DoctorsLetter ParseLetter()
        {
            //Suche Hauptsektion des Dokuments
            XElement letter = SingleByName(document.Descendants(), "section");
            return new DoctorsLetter
            {
                Filepath = file,
                Name = SingleByName(letter.Elements(), "title").Value,
                Diagnosis = SingleByName(letter.Elements(), "text").Value,
                Date = ParseDate(AttributeByName(SingleByName(document.Descendants(), "effectiveTime"), "value").Value),
                Sensitivity = (Sensitivity)SingleByName(letter.Elements(), "confidentialityCode").FirstAttribute.Value[0],
            };
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override IList<Medication> ParseMedications()
        {
            IList<Medication> meds = new List<Medication>();
            //Suche nach Einträgen über Medikationen
            IEnumerable<XElement> entries = FilterByName(document.Descendants(), "substanceAdministration");
            Sensitivity sensitivity = (Sensitivity)SingleByName(SingleByName(document.Descendants(), "section").Elements(), "confidentialityCode").FirstAttribute.Value[0];
            foreach (XElement entry in entries)
            {
                //Parse jeden dieser Einträge in eine Medikation
                meds.Add(ParseMedication(entry, sensitivity));
            }
            return meds;
        }

        /// <summary>
        /// Parst die gegebenen Informationen in eine Medikation
        /// </summary>
        /// <param name="med">Die zu parsenden Informationen über die Medikation</param>
        /// <param name="sensitivity">Die Sensitivitätsstufe der Medikation</param>
        /// <returns>Medikation mit den Informationen aus med</returns>
        private Medication ParseMedication(XElement med, Sensitivity sensitivity)
        {
            XElement dose = SingleByName(med.Elements(), "doseQuantity");
            XElement time = SingleByName(med.Elements(), "effectiveTime");
            XElement phase = SingleByName(time.Elements(), "phase");
            XElement period = SingleByName(time.Elements(), "period");
            Medication target = new Medication()
            {
                Dosis = String.Concat(AttributeByName(dose, "value").Value.ToString(), AttributeByName(dose, "unit").Value),
                Name = SingleByName(med.Descendants(), "name").Value,
                Sensitivity = sensitivity,
                Date = ParseDate(AttributeByName(SingleByName(phase.Elements(), "low"), "value").Value),
                EndDate = ParseDate(AttributeByName(SingleByName(phase.Elements(), "high"), "value").Value),
                Frequency = (int) (1.0/Convert.ToDouble(AttributeByName(period, "value").Value, CultureInfo.InvariantCulture)),
                Interval = (Interval) AttributeByName(period, "unit").Value[0],
            };
            return target;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override Profile ParseProfile()
        {
            //Die Informationen der Profile Klasse sind Informationen über einen Patient.
            XElement patient = SingleByName(document.Descendants(), "patient");
            //Suche nach dem zusammengesetzten legalen Namen
            XElement name = SingleByAttributeName(FilterByName(patient.Elements(), "name"), "use", "L");
            return new Profile
            {
                BirthDate = ParseDate(AttributeByName(SingleByName(patient.Elements(), "birthTime"), "value").Value),
                //Suche den Familiennamen
                Name = SingleByName(name.Elements(), "given").Value,
                //Suche den Vornamen
                LastName = SingleByName(name.Elements(), "family").Value,
                InsuranceNumber = AttributeByName(SingleByName(patient.Elements(), "id"), "extension").Value,
            };
        }

        private IEnumerable<XElement> FilterByAttributeName(IEnumerable<XElement> collection, string name, string value) 
        {
            return collection.Where(v => v.Attributes().First(w => w.Name.LocalName.Equals(name)).Value.Equals(value));
        }

        private XElement SingleByAttributeName(IEnumerable<XElement> collection, string name, string value) 
        {
            return collection.FirstOrDefault(v => v.Attributes().First(w => w.Name.LocalName.Equals(name)).Value.Equals(value));
        }

        private IEnumerable<XElement> FilterByName(IEnumerable<XElement> collection, string name) 
        {
            return collection.Where(v => v.Name.LocalName.Equals(name));
        }

        private XElement SingleByName(IEnumerable<XElement> collection, string name) 
        {
            return collection.FirstOrDefault(v => v.Name.LocalName.Equals(name));
        }

        private XAttribute AttributeByName(XElement element, string name)
        {
            return element.Attributes().FirstOrDefault(v => v.Name.LocalName.Equals(name));
        }

        private DateTime ParseDate(string date)
        {
            return new DateTime(Int32.Parse(date.Substring(0, 4)), Int32.Parse(date.Substring(4, 2)), Int32.Parse(date.Substring(6, 2)));
        }
    }
}
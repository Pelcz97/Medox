using MARC.Everest.Connectors;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using MARC.Everest.Xml;
using myMD.Model.DataModel;
using System.Collections.Generic;
using System.Xml;
using Xamarin.Forms;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse ist ein FileToDatabaseParser für Dateien im .hl7 Format.
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
        /// Initialisiert document, in dem es aus dem gegebenen Dateipfad liest.
        /// </summary>
        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#Init(string)</see>
        protected override void Init(string file)
        {
            XmlStateReader xr = new XmlStateReader(XmlReader.Create(@"C:\path-to-file.xml"));
            XmlIts1Formatter fmtr = new XmlIts1Formatter()
            {
                ValidateConformance = false
            };
            DependencyService.Get<IHl7ParserHelper>().PrepareFormatter(fmtr);
            IFormatterParseResult parseResult = fmtr.Parse(xr, typeof(ClinicalDocument));
            document = parseResult.Structure as ClinicalDocument;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override Doctor ParseDoctor()
        {
            return null;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override DoctorsLetter ParseLetter()
        {
            return null;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override IList<Medication> ParseMedications()
        {
            return null;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile()</see>
        protected override Profile ParseProfile()
        {
            return null;
        }
    }
}
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(myMD.Model.ParserModel.Droid.Hl7ParserHelper))]
namespace myMD.Model.ParserModel.Droid
{
    /// <summary>
    /// Implementierung der IHl7ParserHelper Schnittstelle für Android-Anwendungen
    /// </summary>
    /// <see>myMD.Model.ParserModel.IParserHelper</see>
    [Preserve(AllMembers = true)]
    public class Hl7ParserHelper : IHl7ParserHelper
    {
        /// <summary>
        /// Das Hinzufügen der ClinicalDocumentDatatypeFormatter Instanz zu dem Formatierer erfordert die ICloneable Schnittstelle, die ClinicalDocumentDatatypeFormatter implementiert.
        /// Diese ist jedoch nicht plattformübergreifend verfügbar, weswegen diese Operation plattformspezifisch durchgeührt werden muss.
        /// </summary>
        /// <see>myMD.Model.ParserModel.IParserHelper#PrepareFormatter(MARC.Everest.Formatters.XML.ITS1.XmlIts1Formatter)</see>
        public void PrepareFormatter(XmlIts1Formatter fmtr)
        {
            fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
        }
    }
}
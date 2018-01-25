using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(myMD.Model.ParserModel.Droid.Hl7ParserHelper))]
namespace myMD.Model.ParserModel.Droid
{
    /// <summary>
    /// 
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Hl7ParserHelper : IHl7ParserHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmtr"></param>
        public void PrepareFormatter(XmlIts1Formatter fmtr)
        {
            fmtr.GraphAides.Add(new ClinicalDocumentDatatypeFormatter());
        }
    }
}
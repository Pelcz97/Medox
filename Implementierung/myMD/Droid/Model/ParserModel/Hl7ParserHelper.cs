using MARC.Everest.DataTypes;
using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Linq;
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

        public void FinalizeMedication(SubstanceAdministration source, Medication target)
        {
            PIVL<TS> pivl = source.EffectiveTime.First().Hull as PIVL<TS>;
            target.Date = pivl.Phase.Low.DateValue;
            target.EndDate = pivl.Phase.High.DateValue;
            target.Frequency = pivl.Frequency.Numerator.ToInt();
            target.Interval = (Interval) pivl.Frequency.Denominator.Unit.First();
        }
    }
}
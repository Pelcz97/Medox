﻿using MARC.Everest.Formatters.XML.Datatypes.R1;
using MARC.Everest.Formatters.XML.ITS1;
using Xamarin.Forms;

[assembly: Dependency(typeof(myMD.Model.ParserModel.iOS.Hl7ParserHelper))]
namespace myMD.Model.ParserModel.iOS
{
    /// <summary>
    /// 
    /// </summary>
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
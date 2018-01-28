﻿using MARC.Everest.Formatters.XML.ITS1;
using MARC.Everest.RMIM.UV.CDAr2.POCD_MT000040UV;
using myMD.Model.DataModel;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Schnittstelle für Operationen beim Parsen von .hl7 Dateien, die plattformspezifisch ausgeführt werden müssen.
    /// </summary>
    public interface IHl7ParserHelper
    {
        /// <summary>
        /// Führt nötige Operationen aus um den gegebenen Xml-Formatierer benutzen zu können.
        /// </summary>
        /// <param name="fmtr">Der vorzubereitende Xml-Formatierer</param>
        void PrepareFormatter(XmlIts1Formatter fmtr);

        /// <summary>
        /// List die Werte für Frequency, Interval, Date und EndDate aus source und setzt sie in target.
        /// </summary>
        /// <param name="source">Die Quelle aus der die Informationen gelesen werden sollen</param>
        /// <param name="target">Die Medikation in die die Informationen eingetragen werden sollen</param>
        void FinalizeMedication(SubstanceAdministration source, Medication target);
    }
}
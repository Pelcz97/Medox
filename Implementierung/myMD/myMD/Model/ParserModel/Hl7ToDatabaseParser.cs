using myMD.Model.DataModel;
using System.Collections.Generic;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse ist ein FileToDatabaseParser für Dateien im .hl7 Format.
    /// </summary>
    /// <see>myMD.Model.ParserModel.FileToDatabaseParser</see>
	public class Hl7ToDatabaseParser : FileToDatabaseParser
    {
        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile(string)</see>
        protected override DoctorsLetter ParseLetter(string filename)
        {
            return null;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile(string)</see>
        protected override IList<Medication> ParseMedications(string filename)
        {
            return null;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile(string)</see>
        protected override Doctor ParseDoctor(string filename)
        {
            return null;
        }

        /// <see>myMD.Model.ParserModel.FileToDatabaseParser#ParseProfile(string)</see>
        protected override Profile ParseProfile(string filename)
        {
            return null;
        }
    }
}
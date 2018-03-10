using myMD.Model.DataModel;
using myMD.Model.ParserModel;
using System.Collections.Generic;

namespace myMDTests.Model.ParserModel
{
    internal class TestHl7ToDatabaseParser : Hl7ToDatabaseParser
    {

        new public IList<Medication> ParseMedications() => base.ParseMedications();

        new public DoctorsLetter ParseLetter() => base.ParseLetter();

        new public Profile ParseProfile() => base.ParseProfile();

        new public Doctor ParseDoctor() => base.ParseDoctor();

        new public void Init(string file) => base.Init(file);
    }
}
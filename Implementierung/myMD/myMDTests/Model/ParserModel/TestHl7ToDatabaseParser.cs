using myMD.Model.DataModel;
using myMD.Model.ParserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMDTests.Model.ParserModel
{
    class TestHl7ToDatabaseParser : Hl7ToDatabaseParser
    {
        public TestHl7ToDatabaseParser() : base(new TestHl7ParserHelper()) { }

        new public IList<Medication> ParseMedications() => base.ParseMedications();

        new public DoctorsLetter ParseLetter() => base.ParseLetter();

        new public Profile ParseProfile() => base.ParseProfile();

        new public Doctor ParseDoctor() => base.ParseDoctor();

        new public void Init(string file) => base.Init(file);
    }
}

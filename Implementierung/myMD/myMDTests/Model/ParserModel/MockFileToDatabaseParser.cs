using myMD.Model.DataModel;
using myMD.Model.ParserModel;
using myMDTests.Model.EntityFactory;
using System.Collections.Generic;

namespace myMDTests.Model.ParserModel
{
    public class MockFileToDatabaseParser : FileToDatabaseParser
    {
        public MockFileToDatabaseParser()
        {
            RandomEntityFactory fac = new RandomEntityFactory();
            Doctor = fac.Doctor();
            Letter = fac.Letter();
            Meds = fac.Meds();
            Profile = fac.Profile();
        }

        public Doctor Doctor { get; }

        public DoctorsLetter Letter { get; }

        public List<Medication> Meds { get; }

        public Profile Profile { get; }

        protected override void Init(string file)
        {
        }

        protected override Doctor ParseDoctor() => Doctor;

        protected override DoctorsLetter ParseLetter() => Letter;

        protected override IList<Medication> ParseMedications() => Meds;

        protected override Profile ParseProfile() => Profile;
    }
}
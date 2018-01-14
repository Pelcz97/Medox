using myMD.Model.EntityFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelInterface.DataModelInterface;
using myMD.Model.DataModel;

namespace myMDTests.Model.DataModel
{
    class RandomEntityFactory
    {
        Random r = new Random();

        public IDoctorsLetter ILetter() => Letter();

        public IDoctorsLetterGroup IGroup() => Group();

        public IMedication IMedication() => Medication();

        public IProfile IProfile() => Profile();

        public DoctorsLetter Letter()
        {
            return new DoctorsLetter()
            {
                Date = Date(),
                Name = String(),
                Sensitivity = EnumValue<Sensitivity>(),
                Diagnosis = String(),
                Filepath = String(),
            };
        }

        public DoctorsLetterGroup Group()
        {
            return new DoctorsLetterGroup()
            {
                Date = Date(),
                Name = String(),
                Sensitivity = EnumValue<Sensitivity>(),
            };
        }
        public Medication Medication()
        {
            return new Medication()
            {
                Date = Date(),
                EndDate = Date(),
                Interval = EnumValue<Interval>(),
                Frequency = r.Next(1,5),
                Name = String(),
                Sensitivity = EnumValue<Sensitivity>(),
            };
        }

        public Profile Profile()
        {
            return new Profile() {
                BirthDate = Date(),
                BloodType = EnumValue<BloodType>(),
                InsuranceNumber = String(),
                Name = String(),
                LastName = String(),
            };
        }

        public Doctor Doctor()
        {
            return new Doctor()
            {
                Field = String(),
            };
        }

        public IDoctor IDoctor() => Doctor();

        public DateTime Date()
        {
            int year = r.Next(1000, 9999);
            int month = r.Next(1, 12);
            int day = r.Next(1, DateTime.DaysInMonth(year, month));
            return new DateTime(year, month, day);
        }

        public string String()
        {
            String s = "";
            int length = r.Next(3, 9);
            for (int i = 0; i < length; ++i)
            {
                s += (char)('a' + r.Next(0, 26));
            }
            return s;
        }

        public T EnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }
    }
}

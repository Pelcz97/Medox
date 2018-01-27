using System;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class DoctorsLetterViewModel : OverallViewModel.OverallViewModel
    {
        public DoctorsLetterViewModel(IDoctorsLetter letter)
        {
            this.DoctorsLetter = letter;
        }

        public DoctorsLetterViewModel(object item)
        {
            var letter = (DoctorsLetterViewModel)item;
            this.DoctorsLetter = letter.DoctorsLetter;
        }

        public IDoctorsLetter DoctorsLetter { get; protected set; }

        public IDoctor Doctor { get => this.DoctorsLetter.Doctor; }
        public string DoctorsName { get => this.Doctor.Name; }
        public string DoctorsField { get => this.Doctor.Field; }
        public DateTime DoctorsLetterDate { get => this.DoctorsLetter.Date; }


    }
}

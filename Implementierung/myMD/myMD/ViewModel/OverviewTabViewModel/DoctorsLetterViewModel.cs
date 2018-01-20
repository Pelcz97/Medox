using System;
using myMD.ModelInterface.DataModelInterface;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DoctorsLetterViewModel : OverallViewModel.OverallViewModel
    {
        public DoctorsLetterViewModel(IDoctorsLetter letter)
        {
            this.DoctorsLetter = letter;
        }

        public IDoctorsLetter DoctorsLetter { get; protected set; }

        public IDoctor Doctor { get => this.DoctorsLetter.Doctor; }
        public string DoctorsName { get => this.Doctor.Name; }
        public string DoctorsField { get => this.Doctor.Field; }
        public DateTime DoctorsLetterDate { get => this.DoctorsLetter.Date; }


    }
}

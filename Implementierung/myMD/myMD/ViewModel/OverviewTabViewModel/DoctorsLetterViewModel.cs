using System;
using myMD.ModelInterface.DataModelInterface;


namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DoctorsLetterViewModel : OverallViewModel.OverallViewModel
    {
        public IDoctorsLetter DoctorsLetter { get; set;}

        //public IDoctor Doctor { get => this.DoctorsLetter.Doctor; set => this.DoctorsLetter.Doctor = value; }
        public string DoctorsName { get; set; }
        public string DoctorsField { get; set; }
        public string DoctorsLetterDate { get; set; }


    }
}

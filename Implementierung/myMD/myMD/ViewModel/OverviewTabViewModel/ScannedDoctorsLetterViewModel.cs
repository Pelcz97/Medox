using System;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class ScannedDoctorsLetterViewModel : DoctorsLetterViewModel
    {
        public IDoctorsLetter newDoctorsLetter {get;set;}
        public string newDoctorsName
        {
            get
            {
                return newDoctorsLetter.Doctor.Name;
            }
        }
        public string newDoctorsField { get; set; }
        public DateTime newLetterDate { get; set; }


        public ScannedDoctorsLetterViewModel()
        {
            
        }
    }
}

using System;
using ModelInterface.DataModelInterface;


namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DetailedDoctorsLetterViewModel
    {
        public IDoctorsLetter DoctorsLetter { get; private set;}

        public string DoctorsName { get; set; }
        public string DoctorsField { get; set; }
        public string DoctorsLetterDate { get; set; }
    }
}

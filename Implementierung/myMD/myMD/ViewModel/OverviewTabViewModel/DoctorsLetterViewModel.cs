using System;
using ModelInterface.DataModelInterface;


namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DoctorsLetterViewModel
    {
        public IDoctorsLetter DoctorsLetter { get; private set;}

        public string DoctorsName { get; set; }
        public string DoctorsField { get; set; }
        public string DoctorsLetterDate { get; set; }
    }
}

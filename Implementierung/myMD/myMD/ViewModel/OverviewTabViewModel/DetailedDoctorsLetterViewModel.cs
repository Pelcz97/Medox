using System;
using System.Diagnostics;
using ModelInterface.DataModelInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DetailedDoctorsLetterViewModel : DoctorsLetterViewModel
    {

        public string Diagnosis { get => this.DoctorsLetter.Diagnosis; }

        public DetailedDoctorsLetterViewModel(object e)
        {
            this.DoctorsLetter = e as IDoctorsLetter;

            Debug.WriteLine(this.DoctorsName);
            Debug.WriteLine(this.DoctorsField);
            Debug.WriteLine(this.DoctorsLetterDate);
            Debug.WriteLine("=======================");
        }
    }
}

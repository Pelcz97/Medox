using System;
using ModelInterface.DataModelInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DetailedDoctorsLetterViewModel : DoctorsLetterViewModel
    {

        public string Diagnosis { get => this.DoctorsLetter.Diagnosis; }

        public DetailedDoctorsLetterViewModel(object e)
        {
            this.DoctorsLetter = e as IDoctorsLetter;
        }
    }
}

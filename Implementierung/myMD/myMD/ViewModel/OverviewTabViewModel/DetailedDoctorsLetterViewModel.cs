using System;
namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DetailedDoctorsLetterViewModel : DoctorsLetterViewModel
    {
        public string Diagnosis { get => this.DoctorsLetter.Diagnosis; }
        //public string Medication { get => this.DoctorsLetter; }

        public DetailedDoctorsLetterViewModel()
        {
        }
    }
}

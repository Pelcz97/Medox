using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using myMD.ModelInterface.ModelFacadeInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class DetailedDoctorsLetterViewModel : DoctorsLetterViewModel
    {

        public string Diagnosis { get => this.DoctorsLetter.Diagnosis; }

        public DetailedDoctorsLetterViewModel(object obj) : base(obj)
        {
            var letter = (DoctorsLetterViewModel)obj;
            this.DoctorsLetter = letter.DoctorsLetter;
        }

        public DetailedDoctorsLetterViewModel(IDoctorsLetter letter) : base(letter)
        {
            this.DoctorsLetter = letter;

        }
    }
}

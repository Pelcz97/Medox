using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class DetailedDoctorsLetterViewModel : DoctorsLetterViewModel
    {

        public string Diagnosis { get => this.DoctorsLetter.Diagnosis; }

        public DetailedDoctorsLetterViewModel(object obj) : this(obj as IDoctorsLetter) { }

        public DetailedDoctorsLetterViewModel(IDoctorsLetter letter) : base(letter)
        {
            this.DoctorsLetter = letter;

            /*
            Debug.WriteLine(this.DoctorsName);
            Debug.WriteLine(this.DoctorsField);
            Debug.WriteLine(this.DoctorsLetterDate);
            Debug.WriteLine("=======================");
            */
        }
    }
}

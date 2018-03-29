using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ScannedDoctorsLetterViewModel : OverallViewModel.OverallViewModel
    {
        public string newDoctorsName
        {
            get
            {
                return newDoctorsName;
            }
            set
            {
                Debug.WriteLine("1Möp");
                this.newDoctorsName = value;
                checkProperty();
            }
        }


        public string newDoctorsField
        {
            get
            {
                return newDoctorsField;
            }
            set
            {
                Debug.WriteLine("2Möp");
                newDoctorsField = value;
                checkProperty();
            }
        }

        public DateTime newLetterDate { get; set; }

        public string Diagnosis { get; set; }

        public bool SavingPossible { get; set; }

        public ScannedDoctorsLetterViewModel(string diagnosis)
        {
            Debug.WriteLine("3Möp");
            newLetterDate = DateTime.Today;
            SavingPossible = false;
            OnPropertyChanged("SavingPossible");
            Diagnosis = diagnosis;
            Debug.WriteLine("4Möp");
        }

        public void SaveNewDoctorsLetter(){
            Debug.WriteLine("5Möp");
            ModelFacade.GenerateDoctorsLetter(newDoctorsName, newDoctorsField, newLetterDate, Diagnosis);
            Debug.WriteLine("6Möp");
        }

        void checkProperty(){
            Debug.WriteLine("7Möp");
            SavingPossible = (newDoctorsName != null && newDoctorsField != null);
            OnPropertyChanged("SavingPossible");
            Debug.WriteLine("8Möp");
        }
    }
}

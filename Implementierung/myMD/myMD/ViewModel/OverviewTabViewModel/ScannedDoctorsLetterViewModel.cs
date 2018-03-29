using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ScannedDoctorsLetterViewModel : OverallViewModel.OverallViewModel
    {
        public string NewDoctorsName
        {
            get
            {
                return NewDoctorsName;
            }
            set
            {
                Debug.WriteLine("1Möp");
                this.NewDoctorsName = value;

            }
        }


        public string NewDoctorsField
        {
            get
            {
                return NewDoctorsField;
            }
            set
            {
                Debug.WriteLine("2Möp");
                NewDoctorsField = value;

            }
        }

        public DateTime NewLetterDate { get; set; }

        public string Diagnosis { get; set; }

        public bool SavingPossible { get; set; }

        public ScannedDoctorsLetterViewModel(string diagnosis)
        {
            NewLetterDate = DateTime.Today;
            SavingPossible = false;
            OnPropertyChanged("SavingPossible");
            Diagnosis = diagnosis;

        }

        public void SaveNewDoctorsLetter(){
            Debug.WriteLine("5Möp");
            ModelFacade.GenerateDoctorsLetter(NewDoctorsName, NewDoctorsField, NewLetterDate, Diagnosis);
            Debug.WriteLine("6Möp");
        }

        void checkProperty(){
            Debug.WriteLine("7Möp");
            SavingPossible = (NewDoctorsName != null && NewDoctorsField != null);
            OnPropertyChanged("SavingPossible");
            Debug.WriteLine("8Möp");
        }
    }
}

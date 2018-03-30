using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ScannedDoctorsLetterViewModel : DoctorsLetterViewModel
    {
        public string NewDoctorsName
        {
            get {
                return NewDoctorsName;
            }
            set {
                this.NewDoctorsName = value;
                Debug.WriteLine(NewDoctorsName);
                //SavingPossible = (NewDoctorsName.Length > 0 && NewDoctorsField.Length > 0);
                //OnPropertyChanged("SavingPossible");
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
                
                NewDoctorsField = value;
                Debug.WriteLine(NewDoctorsField);
                //SavingPossible = (NewDoctorsName.Length != 0 && NewDoctorsField.Length != 0);
                //OnPropertyChanged("SavingPossible");
            }
        }

        public DateTime NewLetterDate { get; set; }

        public string Diagnosis { get; set; }

        public bool SavingPossible { get; set; }

        public ScannedDoctorsLetterViewModel(string diagnosis)
        {
            NewLetterDate = DateTime.Today;
            SavingPossible = true;
            OnPropertyChanged("SavingPossible");
            Diagnosis = diagnosis;

        }

        public void SaveNewDoctorsLetter(){
            Debug.WriteLine(NewDoctorsName);
            Debug.WriteLine(NewDoctorsField);
            ModelFacade.GenerateDoctorsLetter(NewDoctorsName, NewDoctorsField, NewLetterDate, Diagnosis);

        }
    }
}

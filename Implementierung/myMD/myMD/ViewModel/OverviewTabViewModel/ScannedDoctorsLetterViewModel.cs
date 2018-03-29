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
                this.NewDoctorsName = value;
                SavingPossible = (NewDoctorsName != null && NewDoctorsField != null);
                OnPropertyChanged("SavingPossible");
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
                SavingPossible = (NewDoctorsName != null && NewDoctorsField != null);
                OnPropertyChanged("SavingPossible");
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
            
            ModelFacade.GenerateDoctorsLetter(NewDoctorsName, NewDoctorsField, NewLetterDate, Diagnosis);

        }
    }
}

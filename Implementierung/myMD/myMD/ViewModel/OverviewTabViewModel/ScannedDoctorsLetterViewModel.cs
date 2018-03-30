using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ScannedDoctorsLetterViewModel : DoctorsLetterViewModel
    {
        string _newDoctorsName { get; set; }
        public string NewDoctorsName
        {
            get { return _newDoctorsName; }
            set
            {
                _newDoctorsName = value;
                SavingPossible = (_newDoctorsName.Length != 0 && NewDoctorsField.Length != 0);
                OnPropertyChanged("SavingPossible");
            }
        }

        string _newDoctorsField { get; set; }
        public string NewDoctorsField
        {
            get { return _newDoctorsField; }

            set
            {
                _newDoctorsField = value;
                SavingPossible = (_newDoctorsName.Length != 0 && _newDoctorsField.Length != 0);
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
            _newDoctorsName = "";
            _newDoctorsField = "";
        }

        public void SaveNewDoctorsLetter(){
            ModelFacade.GenerateDoctorsLetter(NewDoctorsName, NewDoctorsField, NewLetterDate, Diagnosis);
            MessagingCenter.Send(this, "AddNewLetter");
            MessagingCenter.Unsubscribe<ScannedDoctorsLetterViewModel>(this, "AddNewLetter");
        }
    }
}

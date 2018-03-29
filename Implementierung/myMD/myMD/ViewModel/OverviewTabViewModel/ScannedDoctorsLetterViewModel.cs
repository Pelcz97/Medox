using System;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
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
                newDoctorsName = value;
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
                newDoctorsField = value;
                checkProperty();
            }
        }

        public DateTime newLetterDate { get; set; }

        public string Diagnosis { get; set; }

        public bool SavingPossible { get; set; }

        public ScannedDoctorsLetterViewModel(string diagnosis)
        {
            newLetterDate = DateTime.Today;
            SavingPossible = false;
            OnPropertyChanged("SavingPossible");
            Diagnosis = diagnosis;
        }

        public void SaveNewDoctorsLetter(){
            ModelFacade.GenerateDoctorsLetter(newDoctorsName, newDoctorsField, newLetterDate, Diagnosis);
        }

        void checkProperty(){
            SavingPossible = (newDoctorsName != null && newDoctorsField != null);
            OnPropertyChanged("SavingPossible");
        }
    }
}

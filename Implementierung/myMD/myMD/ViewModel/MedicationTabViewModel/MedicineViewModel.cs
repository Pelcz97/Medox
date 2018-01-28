using System;
using myMD.ModelInterface.DataModelInterface;
using System.Diagnostics;
using Xamarin.Forms.Internals;
using System.Collections;

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class MedicineViewModel : OverallViewModel.OverallViewModel
    {

        public IMedication Medication { get; set; }
        public bool SavingPossible { get; set; }
        public bool CancelPossible { get; set; }

        public string MedicationName { 
            get => this.Medication.Name; 
            set { Debug.WriteLine("Name:" + value);
                
                this.Medication.Name = value; 
                SavingPossible = (this.Medication.Name.Length != 0);
                OnPropertyChanged("SavingPossible");} 
        }
        
        public DateTime MedicationStartDate
        {
            get => this.Medication.Date.Date;
            set { Debug.WriteLine("StartDate:" + value.Date); 
                this.Medication.Date = value.Date;
                MinEndDate = Medication.Date;
                calcDuration();
                Debug.WriteLine("Duration:" + MedicationDuration);
                OnPropertyChanged("MinEndDate");
            }
        }

        public DateTime MedicationEndDate { 
            get => this.Medication.EndDate; 
            set { 
                Debug.WriteLine("EndDate:" + value);
                this.Medication.EndDate = value;
                MaxStartDate = Medication.EndDate;
                calcDuration();
                Debug.WriteLine("Duration:" + MedicationDuration);
                OnPropertyChanged("MaxStartDate");
                }
            }

        public DateTime MaxStartDate { get; set; }
        public DateTime MinEndDate { get; set; }

        public int MedicationDuration { set; get; }

        public Interval MedicationInterval { 
            get => this.Medication.Interval; 
            set => this.Medication.Interval = value; }
        
        public int MedicationFrequency { 
            get => this.Medication.Frequency; 
            set { Debug.WriteLine("Frequency:" + value);
                this.Medication.Frequency = value; } }

        public string MedicationDosis { 
            get { return this.Medication.Dosis; } 
            set { this.Medication.Dosis = value; } 
        }

        public MedicineViewModel(IMedication medication)
        {
            Medication = medication;
            calcDuration();
        }

        public MedicineViewModel()
        {
            
        }

        public string NameSort
        {
            get
            {
                /*
                if (string.IsNullOrWhiteSpace(Medication.Name) || Medication.Name.Length == 0)
                    return "?";

                return Medication.Name[0].ToString().ToUpper();
                */
                return Medication.Date.ToString("Y");
            }
        }

        private void calcDuration(){
            MedicationDuration = MedicationEndDate.Subtract(MedicationStartDate).Days + 1;
            OnPropertyChanged("MedicationDuration");
        }

    }
}

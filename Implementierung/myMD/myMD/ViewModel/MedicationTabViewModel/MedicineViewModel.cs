﻿using System;
using myMD.ModelInterface.DataModelInterface;
using System.Diagnostics;
using myMD.ModelInterface.ModelFacadeInterface;


namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicineViewModel : OverallViewModel.OverallViewModel
    {

        public IMedication Medication { get; set; }

        public string MedicationName { 
            get => this.Medication.Name; 
            set { Debug.WriteLine("Name:" + value); 
                this.Medication.Name = value; } 
        }
        
        public DateTime MedicationStartDate
        {
            get => this.Medication.Date.Date;
            set { Debug.WriteLine("StartDate:" + value.Date); 
                this.Medication.Date = value.Date; }
        }

        public DateTime MedicationEndDate { 
            get => this.Medication.EndDate; 
            set { Debug.WriteLine("EndDate:" + value);
                
                    this.Medication.EndDate = value;
                }
            }

        public Interval MedicationInterval { 
            get => this.Medication.Interval; 
            set => this.Medication.Interval = value; }
        
        public int MedicationFrequency { 
            get => this.Medication.Frequency; 
            set { Debug.WriteLine("Frequency:" + value);
                this.Medication.Frequency = value; } }
        //public string MedicationDosis { get; set; }

        public MedicineViewModel(IMedication medication)
        {
            Medication = medication;
        }

        public MedicineViewModel()
        {
            
        }


    }
}

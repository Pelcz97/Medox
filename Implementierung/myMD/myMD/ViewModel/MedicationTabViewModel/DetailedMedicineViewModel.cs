using System;
using myMD.Model.DataModel;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class DetailedMedicineViewModel : MedicineViewModel
    {
        bool OneTimeMedication_Switch { get; set; }
        bool SetDatesPossible { get => !this.OneTimeMedication_Switch; }
        
        public DetailedMedicineViewModel(Medication Medication) : base(Medication)
        {
            this.Medication = Medication;
        }

    }
}

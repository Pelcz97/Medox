using System;
using myMD.ModelInterface.DataModelInterface;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class DetailedMedicineViewModel : MedicineViewModel
    {
        bool OneTimeMedication_Switch { get; set; }
        bool SetDatesPossible { get => !this.OneTimeMedication_Switch; }

        public DetailedMedicineViewModel(IMedication medication) : base(medication)
        {
            this.MedicationStartDate = System.DateTime.Today;
            this.MedicationEndDate = System.DateTime.Today;
            this.Medication = medication;
        }

        public void saveMedication(IMedication Medication){
            
        }
    }
}

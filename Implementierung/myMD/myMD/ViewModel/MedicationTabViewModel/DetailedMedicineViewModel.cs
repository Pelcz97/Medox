using System;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class DetailedMedicineViewModel : MedicineViewModel
    {
        bool OneTimeMedication_Switch { get; set; }
        bool SetDatesPossible { get => !this.OneTimeMedication_Switch; }
        //ModelFacade 

        public DetailedMedicineViewModel(IMedication Medication) : base(Medication)
        {
            this.MedicationStartDate = System.DateTime.Today;
            this.MedicationEndDate = System.DateTime.Today;
            this.Medication = Medication;
        }

        public void saveMedication(IMedication Medication){
            
        }
    }
}

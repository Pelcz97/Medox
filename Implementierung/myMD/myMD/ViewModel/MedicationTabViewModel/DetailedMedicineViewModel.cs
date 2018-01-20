using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class DetailedMedicineViewModel : MedicineViewModel
    {
        bool OneTimeMedication_Switch
        {
            get { return this.OneTimeMedication_Switch; }
            set
            {
                Debug.WriteLine(OneTimeMedication_Switch);
                this.OneTimeMedication_Switch = value;
            }
        }

        bool SetDatesPossible { get => !this.OneTimeMedication_Switch; }

        public DetailedMedicineViewModel(IMedication medication) : base(medication)
        {
            this.Medication = medication;
            this.OneTimeMedication_Switch = false;
            this.MedicationStartDate = System.DateTime.Today;
            this.MedicationEndDate = System.DateTime.Today;
        }

        public DetailedMedicineViewModel() : base()
        {
            //this.Medication = new Medication();
            //this.OneTimeMedication_Switch = false;
            //this.MedicationStartDate = System.DateTime.Today;
            //this.MedicationEndDate = System.DateTime.Today;
        }
    }
}

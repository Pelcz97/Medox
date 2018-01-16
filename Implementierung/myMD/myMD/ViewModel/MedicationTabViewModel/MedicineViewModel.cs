using System;
using ModelInterface.DataModelInterface;
namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicineViewModel
    {

        public IMedication Medication { get; private set; }

        public string MedicationStartDate { get; set; }
        public string MedicationDuration { get; set; }
        public string MedicationName { get; set; }
        public string MedicationDosis { get; set; }
        public string MedicationFrequency { get; set; }

    }
}

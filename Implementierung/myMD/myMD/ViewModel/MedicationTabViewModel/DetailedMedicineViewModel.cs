using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;

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
            Medication = ModelFacade.CreateEmptyMedication();

            MessagingCenter.Subscribe<myMD.View.MedicationTabPages.MedicationPage, object>(this, "SelectedMedication", (sender, arg) => {
                this.Medication = arg as IMedication;
            });
        }

        public void cancelMedication(){
            ModelFacade.Delete(this.Medication);
        }

        public void saveNewMedication()
        {
            ModelFacade.Update(Medication);
            MessagingCenter.Send(this, "SavedMedication");
            MessagingCenter.Unsubscribe<DetailedMedicineViewModel>(this, "SavedMedication");
        }
    }
}

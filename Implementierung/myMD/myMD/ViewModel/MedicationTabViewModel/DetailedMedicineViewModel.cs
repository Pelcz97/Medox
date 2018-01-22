using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;

namespace myMD.ViewModel.MedicationTabViewModel
{

    public class DetailedMedicineViewModel : MedicineViewModel
    {
        
        public DetailedMedicineViewModel(IMedication medication) : base(medication)
        {
            this.CancelPossible = true;
            OnPropertyChanged("CancelPossible");
            this.Medication = medication;
            this.MedicationStartDate = System.DateTime.Today;
            this.MedicationEndDate = System.DateTime.Today;
            this.MedicationDuration = MedicationEndDate.Subtract(MedicationStartDate).Days;
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
        }

        public DetailedMedicineViewModel() : base()
        {
            CancelPossible = true;
            OnPropertyChanged("CancelPossible");
            Medication = ModelFacade.CreateEmptyMedication();
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
            
            MessagingCenter.Subscribe<myMD.View.MedicationTabPages.MedicationPage, object>(this, "SelectedMedication", (sender, arg) =>
            {
                this.Medication = arg as IMedication;
            });
        }

        public DetailedMedicineViewModel(object item)
        {
            CancelPossible = false;
            OnPropertyChanged("CancelPossible");
            MedicineViewModel listItem = (MedicineViewModel)item;
            Medication = listItem.Medication;
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
        }

        public void cancelMedication()
        {
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

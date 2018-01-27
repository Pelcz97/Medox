using System;
using myMD.ModelInterface.DataModelInterface;
using myMD.View.MedicationTabPages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.MedicationTabViewModel
{
    /// <summary>
    /// Detailed medicine view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetailedMedicineViewModel : MedicineViewModel
    { 
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.MedicationTabViewModel.DetailedMedicineViewModel"/> class.
        /// </summary>
        /// <param name="medication">Medication.</param>
        public DetailedMedicineViewModel(IMedication medication) : base(medication)
        {
            this.CancelPossible = true;
            OnPropertyChanged("CancelPossible");
            this.Medication = medication;
            this.MedicationStartDate = DateTime.Today;
            this.MedicationEndDate = DateTime.Today;
            this.MedicationDuration = MedicationEndDate.Subtract(MedicationStartDate).Days;
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.MedicationTabViewModel.DetailedMedicineViewModel"/> class.
        /// </summary>
        public DetailedMedicineViewModel() : base()
        {
            CancelPossible = true;
            OnPropertyChanged("CancelPossible");
            Medication = ModelFacade.CreateEmptyMedication();
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
            
            MessagingCenter.Subscribe<MedicationPage, object>(this, "SelectedMedication", (sender, arg) =>
            {
                this.Medication = arg as IMedication;
            });
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.MedicationTabViewModel.DetailedMedicineViewModel"/> class.
        /// </summary>
        /// <param name="item">Item.</param>
        public DetailedMedicineViewModel(object item)
        {
            CancelPossible = false;
            OnPropertyChanged("CancelPossible");
            MedicineViewModel listItem = (MedicineViewModel)item;
            Medication = listItem.Medication;
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
        }

        /// <summary>
        /// Cancels the medication.
        /// </summary>
        public void cancelMedication()
        {
            ModelFacade.Delete(this.Medication);
        }

        /// <summary>
        /// Saves the new medication.
        /// </summary>
        public void saveNewMedication()
        {
            ModelFacade.Update(Medication);
            MessagingCenter.Send(this, "SavedMedication");
            MessagingCenter.Unsubscribe<DetailedMedicineViewModel>(this, "SavedMedication");
        }
    }
}

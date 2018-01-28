using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.MedicationTabViewModel
{
    /// <summary>
    /// ViewModel einer DetailedMedicine
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetailedMedicineViewModel : MedicineViewModel
    { 
        /// <summary>
        /// Erstellt ein DetailedMedicineViewModel, setzt die Sichtbarkeit 
        /// des Abbrechen-Buttons auf true, legt eine neue, leere Medikation 
        /// in der Datenbank an.  
        /// <see cref="T:myMD.ViewModel.MedicationTabViewModel.DetailedMedicineViewModel"/> class.
        /// </summary>
        public DetailedMedicineViewModel() : base()
        {
            CancelPossible = true;
            OnPropertyChanged("CancelPossible");
            Medication = ModelFacade.CreateEmptyMedication();
            MaxStartDate = Medication.EndDate;
            MinEndDate = Medication.Date;
        }

        /// <summary>
        /// Erstellt ein DetailedMedicineViewModel, setzt die Sichtbarkeit 
        /// des Abbrechen-Buttons auf false, initialisiert die Medikation 
        /// anhand der übergebenen IMedication und berechnet die Einnahmedauer.
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
        /// Bricht den Vorgang ab, indem die neu angelegte Medikation gelöscht 
        /// statt gespeichert wird. Deshalb darf der Abbrechen-Button nicht 
        /// in jedem Kontext sichtbar sein
        /// </summary>
        public void CancelMedication()
        {
            ModelFacade.Delete(Medication);
        }

        /// <summary>
        /// Methode zum Speichern einer Medikation 
        /// </summary>
        public void SaveNewMedication()
        {
            ModelFacade.Update(Medication);
            MessagingCenter.Send(this, "SavedMedication");
            MessagingCenter.Unsubscribe<DetailedMedicineViewModel>(this, "SavedMedication");
        }
    }
}

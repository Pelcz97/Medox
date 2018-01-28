using System;
using myMD.ModelInterface.DataModelInterface;
using System.Diagnostics;
using Xamarin.Forms.Internals;
using System.Collections;

namespace myMD.ViewModel.MedicationTabViewModel
{
    /// <summary>
    /// Das ViewModel einer Medikation. Erleichtert das Binding zum View 
    /// durch das Bereitstellen der Properties
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MedicineViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Die zum MedicineViewModel gehörende Medikation
        /// </summary>
        /// <value>The medication.</value>
        public IMedication Medication { get; set; }

        /// <summary>
        /// Boolean, ob der Speichern-Button sichtbar ist oder nicht
        /// <see cref="T:myMD.ViewModel.MedicationTabViewModel.MedicineViewModel"/> saving possible.
        /// </summary>
        /// <value><c>true</c> if saving possible; otherwise, <c>false</c>.</value>
        public bool SavingPossible { get; set; }

        /// <summary>
        /// Boolean, ob der Abbrechen-Button sichtbar ist oder nicht
        /// <see cref="T:myMD.ViewModel.MedicationTabViewModel.MedicineViewModel"/> cancel possible.
        /// </summary>
        /// <value><c>true</c> if cancel possible; otherwise, <c>false</c>.</value>
        public bool CancelPossible { get; set; }

        /// <summary>
        /// Die Bezeichnung einer Medikation. Ist der zu setzende Name 0 Zeichen lang, 
        /// wird der Speichern-Button deaktiviert.
        /// </summary>
        /// <value>The name of the medication.</value>
        public string MedicationName { 
            get => Medication.Name; 
            set { 
                Medication.Name = value; 
                SavingPossible = (this.Medication.Name.Length != 0);
                OnPropertyChanged("SavingPossible");
            } 
        }

        /// <summary>
        /// Der Einnahmebeginn. Wird der Einnahmebeginn gesetzt, wird zugleich das 
        /// minimale Einnahmeende auf denselben Wert gesetzt, um Überschneidungen zu vermeiden.
        /// Zudem wird die Einnahmedauer neu berechnet.
        /// </summary>
        /// <value>The medication start date.</value>
        public DateTime MedicationStartDate
        {
            get =>Medication.Date;
            set { 
                Medication.Date = value.Date;
                MinEndDate = Medication.Date;
                calcDuration();
                OnPropertyChanged("MinEndDate");
            }
        }

        /// <summary>
        /// Das Einnahmeende. Wird das Einnahmeende gesetzt, wird zugleich der 
        /// maximale Einnahmebeginn auf denselben Wert gesetzt, um Überschneidungen zu vermeiden.
        /// Zudem wird die Einnahmedauer neu berechnet.
        /// </summary>
        /// <value>The medication end date.</value>
        public DateTime MedicationEndDate { 
            get => Medication.EndDate; 
            set {  
                Medication.EndDate = value;
                MaxStartDate = Medication.EndDate;
                calcDuration();
                OnPropertyChanged("MaxStartDate");
                }
            }

        /// <summary>
        /// Das maximale Startdatum.
        /// </summary>
        /// <value>The max start date.</value>
        public DateTime MaxStartDate { get; set; }

        /// <summary>
        /// Das minimale Enddatum.
        /// </summary>
        /// <value>The minimum end date.</value>
        public DateTime MinEndDate { get; set; }

        /// <summary>
        /// Die Dauer einer Einnahme.
        /// </summary>
        /// <value>The duration of the medication.</value>
        public int MedicationDuration { set; get; }

        /// <summary>
        /// Gets or sets the medication interval.
        /// </summary>
        /// <value>The medication interval.</value>
        public Interval MedicationInterval { get; set; }

        /// <summary>
        /// Die Häufigkeit einer Einnahme (täglich).
        /// </summary>
        /// <value>The medication frequency.</value>
        public int MedicationFrequency { get; set; }

        /// <summary>
        /// Die Dosis einer Medikation (Dosierung/Einnahmemenge).
        /// </summary>
        /// <value>The medication dosis.</value>
        public string MedicationDosis { get; set; }

        /// <summary>
        /// Erzeugt ein MedicineViewModel anhand einer übergebenen Medikation.
        /// Berechnet zusätzlich die Einnahmedauer
        /// </summary>
        /// <param name="medication">Medication.</param>
        public MedicineViewModel(IMedication medication)
        {
            Medication = medication;
            calcDuration();
        }

        /// <summary>
        /// Methode zum Gruppieren der Listeneinträge in MedicationViewModel.
        /// Wird zZ nicht eingesetzt, könnte aber noch hilfreich sein.
        /// </summary>
        /// <value>The name sort.</value>
        public string NameSort
        {
            get
            {
                return Medication.Date.ToString("Y");
            }
        }

        /// <summary>
        /// Methode zur Berechnung einer Einnahmedauer. Addiert den Wert 1, 
        /// da eine Medikation, die am selben Tag begonnen und beendet wird, 
        /// nicht 0 sondern 1 Tag lang eingenommen wurde.
        /// </summary>
        private void calcDuration(){
            MedicationDuration = MedicationEndDate.Subtract(MedicationStartDate).Days + 1;
            OnPropertyChanged("MedicationDuration");
        }

    }
}

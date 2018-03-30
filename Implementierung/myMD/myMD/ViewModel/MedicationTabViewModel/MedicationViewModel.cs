using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;
using MvvmHelpers;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms.Internals;
using System;
using System.Collections.Generic;
using myMD.Model.DataModel;
using myMD.Model.MedicationInformation;

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class MedicationViewModel : OverallViewModel.OverallViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// Eine ObservableCollection an MedicineViewModels. 
        /// Dient als Zwischenspeicher der MedicationsItemsList, um Einträge 
        /// der Datenbank hier zunächst abzulegen, bevor sie sortiert und gruppiert werden.
        /// </summary>
        /// <value>The medications list.</value>
        public ObservableCollection<MedicineViewModel> MedicationsList { get; }

        public IList<InteractionPair> MedicationInteractions { get; set; }

        /// <summary>
        /// Eine ObservableCollection an Groupings, die wiederum die einzelnen 
        /// MedicineViewModels und somit die Medications beinhalten. 
        /// Wird zur Gruppierung der Liste benötigt.
        /// </summary>
        /// <value>The medications items list.</value>
        public ObservableCollection<Grouping<string, MedicineViewModel>> MedicationsItemsList { get; set; }

        /// <summary>
        /// Der Schlüssel der MedicationsItemsList, anhand dessen der 
        /// GroupHeader festgelegt wird
        /// </summary>
        /// <value>The key.</value>
        public string Key { get => MedicationsItemsList.FirstOrDefault().Key; }

        /// <summary>
        /// Command, welches das Löschen eines gewählten Listeneintrags initiiert.
        /// </summary>
        /// <value>The delete list item.</value>
        public ICommand DeleteListItem { 
            get {
                
                return new Command((sender) =>
                {
                    DeleteListItemMethod((MedicineViewModel)sender);   
                });
            }
        }

        /// <summary>
        /// Erstellt ein MedicationViewModel, initialisiert die MedicationsList und -ItemsList, 
        /// füllt beide über GroupList() mit den Einträgen der Datenbank auf. 
        /// Abonniert zudem eine Benachrichtigung, falls der Nutzer eine Medikation speichert, um die Liste zu aktualisieren.
        /// </summary>
        public MedicationViewModel()
        {
            MedicationsList = new ObservableCollection<MedicineViewModel>();
            MedicationsItemsList = new ObservableCollection<Grouping<string, MedicineViewModel>>();
            MedicationInteractions = new List<InteractionPair>();
            GroupList();

            MessagingCenter.Subscribe<DetailedMedicineViewModel>(this, "SavedMedication", sender => {
                Reload();
                CheckMed();
            });
        }

        /// <summary>
        /// Methode zum Gruppieren der vorhandenen Medikationen.
        /// Zunächst wird MedicationsList mit MedicineViewModels befüllt, anschließend wird 
        /// die Sortierung (orderby: Jahr, Monat, Tag; absteigend) festgelegt, danach 
        /// das Gruppierungskriterium (group: .Medication.Date.toString("Y"), zb. "September 2017") festegelegt. 
        /// Anhand dessen wird die MedicationsItemsList befüllt. 
        /// Zudem wird die MedicationsItemsList Property benachrichtigt, falls es Änderungen gab.
        /// </summary>
        public void GroupList(){
            
            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }

            var sorted = from medicationItem in MedicationsList
                orderby medicationItem.Medication.Date.Year descending, medicationItem.Medication.Date.Month descending, medicationItem.Medication.Date.Day descending
                group medicationItem by medicationItem.Medication.Date.ToString("Y") into medicationItemGroup
                select new Grouping<string, MedicineViewModel>(medicationItemGroup.Key, medicationItemGroup);

            MedicationsItemsList = new ObservableCollection<Grouping<string, MedicineViewModel>>(sorted);
            OnPropertyChanged("MedicationsItemsList");
        }

        /// <summary>
        /// Methode zum Entfernen einer Medikation aus der Liste:
        /// Der Eintrag wird zuerst aus Datenbank entfernt, anschließend auch aus der MedicationsItemsList, 
        /// indem über die Gruppen und Einträge iteriert wird. 
        /// Denkbar wäre auch ein Reload der Liste, sieht aber seltsam aus.
        /// </summary>
        /// <param name="item">Item.</param>
        public void DeleteListItemMethod(MedicineViewModel item)
        {
            ModelFacade.Delete(item.Medication);
            foreach (Grouping<string,MedicineViewModel> groups in MedicationsItemsList.ToList()) {
                foreach (var obj in groups.ToList()) {
                    if (obj == item){
                        groups.Remove(obj);
                        if (groups.Count == 0){
                            MedicationsItemsList.Remove(groups);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Methode zum Neuladen der Liste
        /// </summary>
        public void Reload()
        {
            MedicationsList.Clear();
            GroupList();
        }

        public async void CheckMed(){

            IList<IMedication> currentMedications = new List<IMedication>();

            foreach (MedicineViewModel med in MedicationsList){
                if (med.Medication.EndDate >= DateTime.Today && med.Medication.Date <= DateTime.Today){
                    currentMedications.Add(med.Medication);
                }
            }

            MedicationInteractions.Clear();

            if (currentMedications.Count() >= 2) {
                
                var interactions = await ModelFacade.GetInteractions(currentMedications);

                if (interactions != null){
                    MedicationInteractions = interactions;
                    Debug.WriteLine(interactions.Count()); 
                }
            }
        }
    }
}


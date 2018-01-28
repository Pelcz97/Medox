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

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class MedicationViewModel : OverallViewModel.OverallViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<MedicineViewModel> MedicationsList { get; }
        public ObservableCollection<Grouping<string, MedicineViewModel>> MedicationsItemsList { get; set; }


        public string Key { get => MedicationsItemsList.FirstOrDefault().Key; }

        private MedicineViewModel _ItemSelected;
        public MedicineViewModel SelectedMedication
        {
            get
            {
                return _ItemSelected;
            }
            set
            {
                if (_ItemSelected != value)
                {
                    _ItemSelected = value;
                    OnPropertyChanged("ItemSelected");
                }
            }
        }        

        public ICommand DeleteListItem { 
            get {
                
                return new Command((sender) =>
                {
                    
                    DeleteListItemMethod((MedicineViewModel)sender);   
                });
            }
        }

        public ICommand RefreshMedicationList {
            get {
                return new Command(() => {
                    MedicationListIsRefreshing = true;
                    Reload();
                    MedicationListIsRefreshing = false;
                });
            }}

        private bool _isRefreshing = false;
        public bool MedicationListIsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("MedicationListIsRefreshing");
            }
        }

        public MedicationViewModel()
        {
            MedicationsList = new ObservableCollection<MedicineViewModel>();
            MedicationsItemsList = new ObservableCollection<Grouping<string, MedicineViewModel>>();
            GroupList();

            MessagingCenter.Subscribe<DetailedMedicineViewModel>(this, "SavedMedication", sender => {
                Reload();
            });
        }

        public void GroupList(){
            
            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }

            var sorted = from medicationItem in MedicationsList
                orderby medicationItem.Medication.Date.Year, medicationItem.Medication.Date.Month, medicationItem.Medication.Date.Day descending
                group medicationItem by medicationItem.Medication.Date.ToString("Y") into medicationItemGroup
                select new Grouping<string, MedicineViewModel>(medicationItemGroup.Key, medicationItemGroup);

            MedicationsItemsList = new ObservableCollection<Grouping<string, MedicineViewModel>>(sorted);
            OnPropertyChanged("MedicationsItemsList");
        }

        public void DeleteListItemMethod(MedicineViewModel item)
        {
            
            MedicationsList.Remove(item);
            //var test = new Grouping<string, MedicineViewModel>(item.Medication.Date.ToString("Y"), item);
            foreach (Grouping<string,MedicineViewModel> groups in MedicationsItemsList) {
                foreach (var obj in groups.ToList()) {
                    if (obj == item){
                        groups.Remove(obj);
                    }
                }
            }

            ModelFacade.Delete(item.Medication);
            //GroupList();
        }

        public void Reload()
        {
            MedicationsList.Clear();
            GroupList();
        }


    }
}


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
                OnPropertyChanged(nameof(MedicationListIsRefreshing));
            }
        }

        public MedicationViewModel()
        {
            MedicationsList = new ObservableCollection<MedicineViewModel>();
            MedicationsItemsList = new ObservableCollection<Grouping<string, MedicineViewModel>>();
            group();

            MessagingCenter.Subscribe<DetailedMedicineViewModel>(this, "SavedMedication", sender => {
                Reload();
            });
        }

        public void group(){
            MedicationsItemsList.Clear();
            MedicationsList.Clear();
            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }
            //Use linq to sorty our monkeys by name and then group them by the new name sort property
            var sorted = from medicationItem in MedicationsList
                orderby medicationItem.Medication.Date.Year, medicationItem.Medication.Date.Month, medicationItem.Medication.Date.Day descending
                group medicationItem by medicationItem.Medication.Date.ToString("Y") into medicationItemGroup
                select new Grouping<string, MedicineViewModel>(medicationItemGroup.Key, medicationItemGroup);

            //create a new collection of groups
            MedicationsItemsList = new ObservableCollection<Grouping<string, MedicineViewModel>>(sorted);
            OnPropertyChanged("MedicationList");
        }

        public void DeleteListItemMethod(object sender)
        {
            var MedicationItem = (MedicineViewModel)sender;
            //var test = new Grouping<string, MedicineViewModel>(MedicationItem.Medication.Date.ToString("Y"), (System.Collections.Generic.IEnumerable<MedicineViewModel>)MedicationItem);
            //MedicationsItemsList.Remove(test);
            ModelFacade.Delete(MedicationItem.Medication);
            group();
        }

        public void Reload()
        {
            MedicationsList.Clear();
            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }
            group();
        }


    }
}


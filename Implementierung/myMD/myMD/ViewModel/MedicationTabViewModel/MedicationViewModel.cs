using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using myMD.ModelInterface.DataModelInterface;
using myMD.Model.DataModel;
using myMD.View.MedicationTabPages;
using Xamarin.Forms;
using myMD.ModelInterface.ModelFacadeInterface;
using System.Diagnostics;
using System.ComponentModel;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationViewModel : OverallViewModel.OverallViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<MedicineViewModel> MedicationsList { get; }

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

        public ICommand AddDummyMed
        {
            get
            {
                return new Command(Reload);
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
            this.MedicationsList = new ObservableCollection<MedicineViewModel>();

            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }

            MessagingCenter.Subscribe<DetailedMedicineViewModel>(this, "SavedMedication", sender => {
                Reload();
            });
        }

        public void DeleteListItemMethod(object sender)
        {
            var MedicationItem = ((MedicineViewModel)sender);
            MedicationsList.Remove(MedicationItem);
            ModelFacade.Delete(MedicationItem.Medication);
        }

        public void Reload()
        {
            MedicationsList.Clear();
            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }
        }


    }
}


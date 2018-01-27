using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;
using MvvmHelpers;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class MedicationViewModel : OverallViewModel.OverallViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<MedicineViewModel> MedicationsList { get; }
        public static ObservableCollection<Grouping<string, MedicineViewModel>> MyItems { get; set; }
        
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

            this.MedicationsList = new ObservableCollection<MedicineViewModel>();

            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }
            group();

            Debug.WriteLine("konstruktor MyItems Size: " + MyItems.Count);
            
            MessagingCenter.Subscribe<DetailedMedicineViewModel>(this, "SavedMedication", sender => {
                Reload();
            });
        }

        public void group(){
            var sorted = from item in MedicationsList
                         orderby item.Medication.Date
                         group item by item.NameSort into itemGroup
                         select new Grouping<string, MedicineViewModel>(itemGroup.Key, itemGroup);

            //create a new collection of groups
            MyItems = new ObservableCollection<Grouping<string, MedicineViewModel>>(sorted);
        }

        public void DeleteListItemMethod(object sender)
        {
            var MedicationItem = ((MedicineViewModel)sender);
            MedicationsList.Remove(MedicationItem);
            //MyItems.Remove(new Grouping<string, MedicineViewModel>(MedicationItem.Key, (System.Collections.Generic.IEnumerable<MedicineViewModel>)MedicationItem));
            Debug.WriteLine("MyItems Size: " + MyItems.Count);
            ModelFacade.Delete(MedicationItem.Medication);
        }

        public void Reload()
        {
            MedicationsList.Clear();
            foreach (IMedication med in ModelFacade.GetAllMedications())
            {
                MedicationsList.Add(new MedicineViewModel(med));
            }
            group();
            Debug.WriteLine("MyItems Size: " + MyItems.Count);
        }


    }
}


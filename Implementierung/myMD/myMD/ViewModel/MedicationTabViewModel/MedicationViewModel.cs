using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using myMD.View.MedicationTabPages;
using Xamarin.Forms;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationViewModel
    {

        public ObservableCollection<MedicineViewModel> MedicationsList { get; }
        public bool MedicationListIsVisible;

        public MedicationViewModel()
        {
            this.MedicationsList = new ObservableCollection<MedicineViewModel>();
            this.MedicationListIsVisible = new bool();

            if (MedicationsList == null) {
                MedicationListIsVisible = false;
            }
        }

    }
}


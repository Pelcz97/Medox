using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ModelInterface.DataModelInterface;
using myMD.Model.DataModel;
using myMD.View.MedicationTabPages;
using Xamarin.Forms;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationViewModel : OverallViewModel.OverallViewModel
    {
        public ObservableCollection<MedicineViewModel> MedicationsList { get; }
        public ICommand AddDummyMed { get; private set; }
        private bool isVisible = true;

        public bool MedicationListIsVisible
        {
            get
            {
                return isVisible;
            }
        }

        public MedicationViewModel()
        {
            this.MedicationsList = new ObservableCollection<MedicineViewModel>();

            this.AddDummyMed = new Command((sender) =>
            {
                MedicineViewModel test = new MedicineViewModel(new Medication());
            });
        }





    }
}


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

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationViewModel : OverallViewModel.OverallViewModel
    {
        public IList<MedicineViewModel> MedicationsList { get; }
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
            this.MedicationsList = (IList<MedicineViewModel>)ModelFacade.GetAllMedications();

            this.AddDummyMed = new Command((sender) =>
            {
                
            });
        }





    }
}


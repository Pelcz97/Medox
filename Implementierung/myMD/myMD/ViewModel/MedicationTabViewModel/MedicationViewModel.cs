using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using myMD.View.MedicationTabPages;
using Xamarin.Forms;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationViewModel
    {
        IList Medications = new List<MedicineViewModel>();
        private bool isVisible;

        public bool MedicationListIsVisible
        {
            get
            {
                return isVisible;
            }
        }

        public MedicationViewModel()
        {
            if (Medications.Count > 0)
            {
                this.isVisible = true;
            }
            else
            {
                this.isVisible = false;
            }
        }



    }
}


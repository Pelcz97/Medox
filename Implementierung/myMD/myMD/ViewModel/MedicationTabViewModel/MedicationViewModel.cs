﻿using System;
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
                MedicineViewModel test = new MedicineViewModel();
                test.MedicationName = "Paracetamol";
                test.MedicationDosis = "500mg";
                test.MedicationDuration = "4 Tage lang";
                test.MedicationFrequency = "3 mal täglich";
                test.MedicationStartDate = "08. Januar 2018";
                MedicationsList.Add(test);
            });
        }





    }
}

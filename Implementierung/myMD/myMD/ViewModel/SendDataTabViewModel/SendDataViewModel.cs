﻿using System;
using System.Diagnostics;
using myMD.Model.DependencyService;
using myMD.Model.TransmissionModel;
using nexus.protocols.ble;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Die SendDataViewModel Klasse bietet den Einstiegspunkt in den SendDataTab.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SendDataViewModel : OverallViewModel.OverallViewModel
    {
        public bool ReceiveLettersButton_Enabled { get; set; }

        /// <summary>
        /// Konstruktor für ein SendDataViewModel
        /// </summary>
        public SendDataViewModel()
        {
            ReceiveLettersButton_Enabled = true;
            /*UpdateButton();

            if (DependencyServiceWrapper.Get<IBluetoothHelper>().Adapter.CurrentState.Value != EnabledDisabledState.Enabled)
            {
                DependencyServiceWrapper.Get<IBluetoothHelper>().Adapter.CurrentState.Subscribe(state => {
                    UpdateButton();
                });
            }*/
        }

        
        private void UpdateButton(){
            ReceiveLettersButton_Enabled = (DependencyServiceWrapper.Get<IBluetoothHelper>().Adapter != null
                                            && DependencyServiceWrapper.Get<IBluetoothHelper>().Adapter.CurrentState.Value == EnabledDisabledState.Enabled);
            OnPropertyChanged("ReceiveLettersButton_Enabled");
        }
        
    }
}

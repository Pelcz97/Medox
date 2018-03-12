using System;
using System.Diagnostics;
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
            UpdateButton();

            if (BluetoothAdapter.CurrentState.Value != EnabledDisabledState.Enabled)
            {
                BluetoothAdapter.CurrentState.Subscribe(state => {
                    UpdateButton();
                });
            }
        }

        private void UpdateButton(){
            ReceiveLettersButton_Enabled = (BluetoothAdapter != null
                                            && BluetoothAdapter.CurrentState.Value == EnabledDisabledState.Enabled);
            OnPropertyChanged("ReceiveLettersButton_Enabled");
        }
    }
}

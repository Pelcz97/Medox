using System.Diagnostics;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Die SendDataViewModel Klasse bietet den Einstiegspunkt in den SendDataTab.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SendDataViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Konstruktor für ein SendDataViewModel
        /// </summary>
        public SendDataViewModel()
        {
            //Debug.WriteLine(BluetoothAdapter.CurrentState);
        }
    }
}

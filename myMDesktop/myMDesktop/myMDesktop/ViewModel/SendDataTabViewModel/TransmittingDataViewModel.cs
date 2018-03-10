using Xamarin.Forms.Internals;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverallViewModel;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System.Text;
using System.Reactive.Linq;
using ReactiveUI;
using System.Collections.Generic;
using myMDesktop.Model.TransmissionModel;
using Plugin.FilePicker.Abstractions;
using System.Linq;
using System.IO;

namespace myMDesktop.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel
    {
        Collection<IEnumerable<byte[]>> SplittedFiles { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            SplittedFiles = new Collection<IEnumerable<byte[]>>();
            StartServer();

            MessagingCenter.Subscribe<SelectDoctorsLettersViewModel, ObservableCollection<FileData>>(this, "SelectedLetters", (sender, arg) => {

                SplittedFiles.Clear();
                foreach (FileData file in arg)
                {
                    SplittedFiles.Add(SplitFile(file));
                }
                
                DependencyService.Get<IServer>().SplittedFiles = SplittedFiles;
            });


        }

        public async void StartServer()
        {
            await DependencyService.Get<IServer>().StartServer();
        }

        public IEnumerable<byte[]> SplitFile(FileData file)
        {
            var array = file.DataArray;
            Debug.WriteLine(BitConverter.ToString(array));
            
            var batchSize = 180; //534?
            var batched = array
                .Select((Value, Index) => new { Value, Index })
                .GroupBy(p => p.Index / batchSize)
                .Select(g => g.Select(p => p.Value).ToArray());

            Debug.WriteLine("Batched: " + batched.Count());
            
            return batched;
        }

        private string ListToString(List<byte[]> list)
        {
            StringBuilder result = new StringBuilder();
            foreach (byte[] file in list)
            {
                result.Append(Encoding.UTF8.GetString(file, 0, file.Length));
            }
            return result.ToString();
        }
    }
}

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
                DependencyService.Get<IServer>().DoctorsLetters = arg;

                foreach (FileData file in arg)
                {
                    SplittedFiles.Add(SplitFile(file));
                }

                DependencyService.Get<IServer>().SplittedFiles = SplittedFiles;

                Debug.WriteLine(DependencyService.Get<IServer>().DoctorsLetters.Count);
            });


        }

        public async void StartServer()
        {
            await DependencyService.Get<IServer>().StartServer();
        }

        public IEnumerable<byte[]> SplitFile(FileData file)
        {
            var array = file.DataArray;

            var batchSize = 500; //534?
            var batched = array
                .Select((Value, Index) => new { Value, Index })
                .GroupBy(p => p.Index / batchSize)
                .Select(g => g.Select(p => p.Value).ToArray());

            Debug.WriteLine("Batch size " + batched.Count());
            return batched;
        }
    }
}

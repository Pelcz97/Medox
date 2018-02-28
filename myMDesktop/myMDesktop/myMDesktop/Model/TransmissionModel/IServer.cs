using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace myMDesktop.Model.TransmissionModel
{
    [Preserve(AllMembers = true)]
    public interface IServer
    {
        Task StartServer();
        Collection<IEnumerable<byte[]>> SplittedFiles { get; set; }
        ObservableCollection<FileData> DoctorsLetters
        {
            get;
            set;
        }
    }
}

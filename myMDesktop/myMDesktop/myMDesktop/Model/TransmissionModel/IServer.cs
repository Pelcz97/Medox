using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace myMDesktop.Model.TransmissionModel
{
    [Preserve(AllMembers = true)]
    public interface IServer
    {
       Task StartServer();
       List<int> DoctorsLetters { get; set; }
    }
}

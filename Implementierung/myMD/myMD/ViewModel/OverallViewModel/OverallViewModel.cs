using System.ComponentModel;
using System.Runtime.CompilerServices;
using myMD.ModelInterface.ModelFacadeInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverallViewModel
{
    [Preserve(AllMembers = true)]
    public class OverallViewModel : INotifyPropertyChanged
    {
        protected IModelFacade ModelFacade;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OverallViewModel()
        {
            ModelFacade = App.Model;
        }


    }
}

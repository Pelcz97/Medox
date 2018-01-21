using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.OverallViewModel
{
    public class OverallViewModel : INotifyPropertyChanged
    {
        protected IModelFacade ModelFacade;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public OverallViewModel()
        {
            ModelFacade = App.GetModel();
        }


    }
}

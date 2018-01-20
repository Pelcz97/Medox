using System;
using System.Diagnostics;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.OverallViewModel
{
    public class OverallViewModel
    {
        protected IModelFacade ModelFacade;

        public OverallViewModel()
        {
            ModelFacade = App.GetModel();
        }
    }
}

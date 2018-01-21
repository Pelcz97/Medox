using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using myMD.View.OverviewTabPages;
using Xamarin.Forms;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class OverviewViewModel : OverallViewModel.OverallViewModel
    {
        INavigation Navigation { get; set; }


        public ObservableCollection<DoctorsLetterViewModel> DoctorsLettersList { get; }
        public ICommand EditDoctorsLettersList_Clicked { get; private set; }

        public OverviewViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;

            this.DoctorsLettersList = new ObservableCollection<DoctorsLetterViewModel>();

            this.EditDoctorsLettersList_Clicked = new Command((sender) =>
            {
                /*
                DoctorsLetterViewModel test = new DoctorsLetterViewModel();
                test.DoctorsField = "Hausarzt";
                test.DoctorsLetterDate = "29. September 2017";
                test.DoctorsName = "Dr. Peter Platzhalter";
                DoctorsLettersList.Add(test);
                */
                
            });
        }
    }
}

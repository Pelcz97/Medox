using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;


namespace myMD.ViewModel.OverviewTabViewModel
{
    public class OverviewViewModel
    {

        public ObservableCollection<DetailedDoctorsLetterViewModel> DoctorsLettersList { get; }
        public ICommand EditDoctorsLettersList_Clicked { get; private set; }

        private bool isVisible = true;

        public bool DoctorsLettersListIsVisible
        {
            get
            {
                return isVisible;
            }
        }

        public OverviewViewModel()
        {
            this.DoctorsLettersList = new ObservableCollection<DetailedDoctorsLetterViewModel>();

            this.EditDoctorsLettersList_Clicked = new Command((sender) =>
            {
                DetailedDoctorsLetterViewModel test = new DetailedDoctorsLetterViewModel();
                test.DoctorsField = "Hausarzt";
                test.DoctorsLetterDate = "29. September 2017";
                test.DoctorsName = "Dr. Peter Platzhalter";
                DoctorsLettersList.Add(test);
            });
        }


    }
}

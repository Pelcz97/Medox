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


        public OverviewViewModel()
        {
            this.DoctorsLettersList = new ObservableCollection<DetailedDoctorsLetterViewModel>();
            this.EditDoctorsLettersList_Clicked = new Command((sender) =>
            {
                DetailedDoctorsLetterViewModel test = new DetailedDoctorsLetterViewModel();
                test.DoctorsField = "Test";
                test.DoctorsLetterDate = "29.05.1998";
                test.DoctorsName = "Dr. Dellnitz";
                DoctorsLettersList.Add(test);
            });
        }
    }
}

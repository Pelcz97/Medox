using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using myMD.View.OverviewTabPages;
using Xamarin.Forms;
using myMD.ModelInterface.ModelFacadeInterface;
using Xamarin.Forms.Internals;
using myMD.Model.DataModel;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class OverviewViewModel : OverallViewModel.OverallViewModel
    {
        INavigation Navigation { get; set; }


        public ObservableCollection<DoctorsLetterViewModel> DoctorsLettersList { get; }
        public ICommand EditDoctorsLettersList_Clicked { get { return new Command(() => { }); } }
        public ICommand AddDummyLetter { 
            get { 
                return new Command(() => {
                    var dummyLetter = new DoctorsLetter();
                    var dummyDoc = new Doctor();
                    dummyDoc.Name = "Harald Helfgott";
                    dummyDoc.Field = "Podologe";
                    dummyLetter.Date = DateTime.Now.Date;
                    dummyLetter.DatabaseDoctor = dummyDoc;
                    dummyLetter.Diagnosis = "Diarrhö";
                    var dummyItem = new DoctorsLetterViewModel(dummyLetter);
                    DoctorsLettersList.Add(dummyItem); 
                }); } }

        public OverviewViewModel()
        {
            this.DoctorsLettersList = new ObservableCollection<DoctorsLetterViewModel>();
        }
    }
}

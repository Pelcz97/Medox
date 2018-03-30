using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using MvvmHelpers;
using System.Linq;
using myMD.ViewModel.SendDataTabViewModel;

namespace myMD.ViewModel.OverviewTabViewModel
{
    /// <summary>
    /// ViewModel zur Hauptseite des OverviewTabs. Hier wird hauptsächlich die Liste an Arztbriefen modeliert.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class OverviewViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Die Liste der vorhandenen Arztbriefe.
        /// </summary>
        /// <value>The doctors letters list.</value>
        public ObservableCollection<DoctorsLetterViewModel> DoctorsLettersList { get; }

        public ObservableCollection<Grouping<string, DoctorsLetterViewModel>> DoctorsLettersItemsList { get; set; }

        /// <summary>
        /// Der Schlüssel der MedicationsItemsList, anhand dessen der 
        /// GroupHeader festgelegt wird
        /// </summary>
        /// <value>The key.</value>
        public string Key { get => DoctorsLettersItemsList.FirstOrDefault().Key; }


        /// <summary>
        /// Command um ein DoctorsLetter aus der Liste zu löschen
        /// </summary>
        public ICommand DeleteListItem
        {
            get
            {
                return new Command((sender) =>
                {
                    DeleteListItemMethod((DoctorsLetterViewModel)sender);
                });
            }
        }

        /// <summary>
        /// Erzeugt ein OverviewViewModel. Zunächst wird die DoctorsLettersList als neue leere Liste initialisiert, 
        /// danach werden alle in der Datenbank gespeicherten Arztbriefe als DoctorsLetterViewModel in die Liste eingefügt.
        /// </summary>
        public OverviewViewModel()
        {
            DoctorsLettersList = new ObservableCollection<DoctorsLetterViewModel>();
            DoctorsLettersItemsList = new ObservableCollection<Grouping<string, DoctorsLetterViewModel>>();
            GroupList();

            MessagingCenter.Subscribe<TransmittingDataViewModel>(this, "UpdateDoctorsLettersList", sender => {
                Reload();
            });
            MessagingCenter.Subscribe<ScannedDoctorsLetterViewModel>(this, "AddNewLetter", sender => {
                Reload();
            });
        }

        void GroupList()
        {
            foreach (IDoctorsLetter letter in ModelFacade.GetAllDoctorsLetters())
            {
                DoctorsLettersList.Add(new DoctorsLetterViewModel(letter));
            }

            var sorted = from letterItem in DoctorsLettersList
                orderby letterItem.DoctorsLetter.Date.Year descending, 
                        letterItem.DoctorsLetter.Date.Month descending, 
                        letterItem.DoctorsLetter.Date.Day descending
            group letterItem by letterItem.DoctorsLetter.Date.ToString("Y") into letterItemGroup
            select new Grouping<string, DoctorsLetterViewModel>(letterItemGroup.Key, letterItemGroup);

            DoctorsLettersItemsList = new ObservableCollection<Grouping<string, DoctorsLetterViewModel>>(sorted);
            OnPropertyChanged("DoctorsLettersItemsList");
        }

        void DeleteListItemMethod(DoctorsLetterViewModel item)
        {
            ModelFacade.Delete(item.DoctorsLetter);
            foreach (Grouping<string, DoctorsLetterViewModel> groups in DoctorsLettersItemsList.ToList())
            {
                foreach (var obj in groups.ToList())
                {
                    if (obj == item)
                    {
                        groups.Remove(obj);
                        if (groups.Count == 0)
                        {
                            DoctorsLettersItemsList.Remove(groups);
                        }
                    }
                }
            }
        }

        void Reload()
        {
            DoctorsLettersList.Clear();
            GroupList();
        }
    }
}

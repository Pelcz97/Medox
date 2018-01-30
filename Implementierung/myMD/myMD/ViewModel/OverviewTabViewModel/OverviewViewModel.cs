using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;

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

        /// <summary>
        /// Command, das ein Dummy Element mit den gegebenen festen Werten erstellt.
        /// Nur zu Testzwecken.
        /// </summary>
        /// <value>The add dummy letter.</value>
        public ICommand AddDummyLetter { 
            get { 
                return new Command(() => {
                    var dummyLetter = new DoctorsLetter();
                    var dummyDoc = new Doctor();
                    dummyDoc.Name = "Dr. Harald Helfgott";
                    dummyDoc.Field = "Podologe";
                    dummyLetter.Date = DateTime.Now.Date;
                    dummyLetter.DatabaseDoctor = dummyDoc;
                    dummyLetter.Diagnosis = "Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr, sed diam nonumy eirmod tempor invidunt " +
                        "ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est " +
                        "Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr, sed diam nonumy eirmod tempor invidunt " +
                        "ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est " +
                        "Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr, sed diam nonumy eirmod tempor invidunt " +
                        "ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est " +
                        "Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr, sed diam nonumy eirmod tempor invidunt " +
                        "ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem " +
                        "ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
                        "sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut " +
                        "labore et dolore magna aliquyam erat, sed diam voluptua. " +
                        "At vero eos et accusam et justo duo dolores et ea rebum. " +
                        "Stet clita kasd gubergren, no sea takimata sanctus est " +
                        "Lorem ipsum dolor sit amet.";
                    
                    var dummyItem = new DoctorsLetterViewModel(dummyLetter);
                    DoctorsLettersList.Add(dummyItem); 
                }); } }

        /// <summary>
        /// Erzeugt ein OverviewViewModel. Zunächst wird die DoctorsLettersList als neue leere Liste initialisiert, 
        /// danach werden alle in der Datenbank gespeicherten Arztbriefe als DoctorsLetterViewModel in die Liste eingefügt.
        /// </summary>
        public OverviewViewModel()
        {
            this.DoctorsLettersList = new ObservableCollection<DoctorsLetterViewModel>();
            foreach (IDoctorsLetter letter in ModelFacade.GetAllDoctorsLetters())
            {
                DoctorsLettersList.Add(new DoctorsLetterViewModel(letter));
            }
        }
    }
}

using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    /// <summary>
    /// Detailed doctors letter view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetailedDoctorsLetterViewModel : DoctorsLetterViewModel
    {
        /// <summary>
        /// Die im Arztbrief gespeicherte Diagnose.
        /// </summary>
        /// <value>The diagnosis.</value>
        public string Diagnosis { get => DoctorsLetter.Diagnosis; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.OverviewTabViewModel.DetailedDoctorsLetterViewModel"/> class.
        /// </summary>
        /// <param name="obj">Object.</param>
        public DetailedDoctorsLetterViewModel(object obj) : base(obj)
        {
            var letter = (DoctorsLetterViewModel)obj;
            DoctorsLetter = letter.DoctorsLetter;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.OverviewTabViewModel.DetailedDoctorsLetterViewModel"/> class.
        /// </summary>
        /// <param name="letter">Letter.</param>
        public DetailedDoctorsLetterViewModel(IDoctorsLetter letter) : base(letter)
        {
            DoctorsLetter = letter;

        }
    }
}

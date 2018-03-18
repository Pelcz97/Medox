using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    /// <summary>
    /// ViewModel zur Modelierung eines DoctorsLetter
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DoctorsLetterViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Der Arztbrief des DoctorsLetterViewModels.
        /// </summary>
        /// <value>The doctors letter.</value>
        public IDoctorsLetter DoctorsLetter { get; protected set; }

        /// <summary>
        /// Der Arzt, der den Arztbrief verfasst hat.
        /// </summary>
        /// <value>The doctor.</value>
        public IDoctor Doctor { get => DoctorsLetter.Doctor; }

        /// <summary>
        /// Der Name des Arztes, der den Arztbrief verfasst hat.
        /// </summary>
        /// <value>The name of the doctors.</value>
        public string DoctorsName { get => Doctor.Name; }

        /// <summary>
        /// Das berufsfeld des Arztes, der den Arztbrief verfasst hat.
        /// </summary>
        /// <value>The doctors field.</value>
        public string DoctorsField { get => Doctor.Field; }

        /// <summary>
        /// Das Ausstellungsdatum des Arztbriefes.
        /// </summary>
        /// <value>The doctors letter date.</value>
        public DateTime DoctorsLetterDate { get => DoctorsLetter.Date; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.ViewModel.OverviewTabViewModel.DoctorsLetterViewModel"/> class.
        /// </summary>
        /// <param name="letter">Letter.</param>
        public DoctorsLetterViewModel(IDoctorsLetter letter)
        {
            DoctorsLetter = letter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.ViewModel.OverviewTabViewModel.DoctorsLetterViewModel"/> class.
        /// </summary>
        /// <param name="item">Item.</param>
        public DoctorsLetterViewModel(object item)
        {
            var letter = (DoctorsLetterViewModel)item;
            DoctorsLetter = letter.DoctorsLetter;
            Debug.WriteLine(letter.DoctorsLetter.Diagnosis);
        }
    }
}


using myMDesktop.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Plugin.FilePicker;
using System;
using Plugin.FilePicker.Abstractions;
using System.Diagnostics;

namespace myMDesktop.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur SelectDoctorsLettersPage
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class SelectDoctorsLettersPage : ContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        SelectDoctorsLettersViewModel vm;

        /// <summary>
        /// Kosntruktor für eine SelectDoctorsLetterPage
        /// </summary>
        public SelectDoctorsLettersPage()
        {
            InitializeComponent();
            vm = new SelectDoctorsLettersViewModel();
            BindingContext = vm;
            vm.DoctorsLetters.Clear();
        }

        /// <summary>
        /// Methode um eine SelectDoctosLetterPage zu schließen
        /// </summary>
        /// <param name="sender">Der Sender der diese Methode auslöst</param>
        /// <param name="e">Das Event, dass des Senders</param>
        async void CancelSelectDoctorsLetters_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        async void ConfirmSelectDoctorsLetters_Clicked(object sender, System.EventArgs e)
        {
            vm.SelectionConfirmed();
            await Navigation.PopModalAsync();
        }

        async void PickFile_Clicked(object sender, System.EventArgs e)
        {
            try
            {

                FileData filedata = await CrossFilePicker.Current.PickFile();
                InsertFiletoVM(filedata);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public async void InsertFiletoVM(FileData filedata)
        {
            bool worked = false;
            if (filedata.FileName.EndsWith(".hl7"))
            {
                if (vm.DoctorsLetters.Count != 0)
                {

                    foreach (FileData letter in vm.DoctorsLetters)
                    {
                        if (letter.FileName != filedata.FileName)
                        {
                            worked = true;
                        }
                        else
                        {
                            await DisplayAlert("InputError", "Diese Datei wurde bereits ausgewählt", "OK");
                            worked = false;
                            break;
                        }
                    }
                }
                else
                {
                    vm.DoctorsLetters.Add(filedata);
                    Debug.WriteLine(System.Text.Encoding.Default.GetString(filedata.DataArray));
                }
            }

            else
            {
                await DisplayAlert("Input Error", "Sie dürfen nur .hl7 Dateien auswählen", "OK");
            }

            if (worked)
            {
                vm.DoctorsLetters.Add(filedata);
                Debug.WriteLine(System.Text.Encoding.Default.GetString(filedata.DataArray));
            }
        }
    }
}
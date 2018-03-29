using myMD.Model.DependencyService;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class KameraViewModel : OverallViewModel.OverallViewModel
    {

        public Image Image { get; set; }
        public MediaFile File { get; set; }
        public bool ScanEnabled { get; set; }

        public KameraViewModel(){
            ScanEnabled = false;
            OnPropertyChanged("ScanEnabled");
            Debug.WriteLine(File != null);
        }

        public async Task<ImageSource> TakePhoto(){
            File = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Rear,
                AllowCropping = true,
                RotateImage = false,
                SaveMetaData = false
            });

            return GenerateImageSource(File);
        }

        public async Task<ImageSource> PickPhoto()
        {
            File = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                SaveMetaData = false
            });

            return GenerateImageSource(File);
        }

        ImageSource GenerateImageSource(MediaFile file){
            

            if (file == null){
                ScanEnabled = false;
                OnPropertyChanged("ScanEnabled");
                return null;
            }

            var source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            ScanEnabled = true;
            OnPropertyChanged("ScanEnabled");
            return source;
        }

        public Task<string> ScanImage(){
            return ModelFacade.GetTextFromImage(GetImageAsByteArray(File));
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="file">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                return memoryStream.ToArray();
            }
        }


    }
}
﻿using myMD.Model.DependencyService;
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

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class KameraViewModel : OverallViewModel.OverallViewModel
    {

        public Image Image { get; set; }
        public MediaFile File { get; set; }

        public ICommand ScanImageButton
        {
            get
            {
                return new Command((sender) =>
                {
                    Debug.WriteLine("möp");
                    ModelFacade.GenerateDoctorsLetterFromImage(GetImageAsByteArray(File));
                });
            }
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
            if (File == null)
                return null;

            var source = ImageSource.FromStream(() =>
            {
                var stream = File.GetStream();
                File.Dispose();
                return stream;
            });

            return source;
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
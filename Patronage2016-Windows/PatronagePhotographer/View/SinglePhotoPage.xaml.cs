using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Media.Capture;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PatronagePhotographer.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SinglePhotoPage : Page
    {
        public SinglePhotoPage()
        {
            this.InitializeComponent();
        }

        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(ShareImageHandler);
        }

        private void ShareImageHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {

        }

        private async void Camera_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.AllowCropping = false;

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            StorageFolder storageFolder = KnownFolders.CameraRoll;

            if (photo == null || storageFolder == null)
            {
                // Użytkownik anulował robienie zdjęcia
                return;
            }

            StorageFile storageFile = await storageFolder.CreateFileAsync(
              "PatronagePhoto.jpg",
              CreationCollisionOption.GenerateUniqueName);

            using (Stream outputStream = await storageFile.OpenStreamForWriteAsync())
            {
                using (Stream photoStream = await photo.OpenStreamForReadAsync())
                    await photoStream.CopyToAsync(outputStream);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

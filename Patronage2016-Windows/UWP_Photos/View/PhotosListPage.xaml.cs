using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.Storage.Streams;

using UWP_Photos.Model;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Photos.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotosListPage : Page
    {
        private ObservableCollection<Photo> _photos;

        public ObservableCollection<Photo> Photos
        {
            get
            {
                return _photos;
            }

            set
            {
                _photos = value;
            }
        }

        public PhotosListPage()
        {
            InitializeComponent();
            Photos = new ObservableCollection<Photo>();
            PhotoManager.GetAllPhotos(Photos);
        }

        private void ShareButton_Click(object sender, RoutedEventArgs e)
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

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}

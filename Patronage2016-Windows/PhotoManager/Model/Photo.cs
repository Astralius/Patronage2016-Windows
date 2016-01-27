using System;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoManager.Model
{
    public class Photo
    {
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public DateTime DateTaken { get; set; }
        public string ImageSource { get; set; }
    }

    public class PhotoService
    {
        //public async static Task<List<Photo>> GetPhotos()
        //{
        //    Debug.WriteLine("GET for Photos");

        //    IReadOnlyList<IStorageItem> itemsList = await KnownFolders.CameraRoll();
        //    Item                 
        //}

        //public async 
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWP_Photos.Model
{
    public class PhotoManager
    {
        public static void GetAllPhotos(ObservableCollection<Photo> photos)
        {
            List<Photo> allPhotos = getPhotos();
            photos.Clear();
            if (allPhotos.Count > 0)
            {
                allPhotos.ForEach(p => photos.Add(p));
            }
        }

        private static List<Photo> getPhotos()
        {
            var photos = new List<Photo>();

            var task = getPhotosAsync();
            task.Wait();
            IReadOnlyList<StorageFile> filesList = task.Result;

            foreach (StorageFile item in filesList)
            {
                Photo p = new Photo(item);
                photos.Add(p);
            }
            return photos;
        }

        private static async Task<IReadOnlyList<StorageFile>> getPhotosAsync()
        {
            return await KnownFolders.CameraRoll.GetFilesAsync();
        }
    }
}

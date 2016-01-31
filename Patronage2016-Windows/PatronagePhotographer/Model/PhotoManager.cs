using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace PatronagePhotographer.Model
{
    public class PhotoManager
    {
        public static void GetAllPhotos(ObservableCollection<Photo> photos)
        {
            var allPhotos = GetPhotos();
            photos.Clear();
            if (allPhotos.Count > 0)
            {
                allPhotos.ForEach(photos.Add);
            }
        }

        private static List<Photo> GetPhotos()
        {
            var task = GetPhotosAsync();
            if(!task.IsCompleted)
                task.Wait();
            var filesList = task.Result;

            return filesList.Select(item => new Photo(item)).ToList();
        }

        private static async Task<IReadOnlyList<StorageFile>> GetPhotosAsync()
        {
            return await KnownFolders.CameraRoll.GetFilesAsync();
        }
    }
}

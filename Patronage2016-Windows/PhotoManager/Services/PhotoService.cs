using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoManager.Services
{
    public class PhotoService : IPhotoService
    {
        public IEnumerable<string> GetPhotosPaths()
        {
            var items = KnownFolders.CameraRoll.GetFilesAsync().GetAwaiter().GetResult();
            return items.Select(item => item.Path).ToList();
        }
    }
}

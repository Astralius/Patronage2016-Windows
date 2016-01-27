using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoManager.Services
{
    public interface IPhotoService
    {
        IEnumerable<string> GetPhotosPaths();
    }
}

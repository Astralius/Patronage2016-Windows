using PhotoManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.ViewModel
{
    public class PhotoViewModel
    {
        public ObservableCollection<Photo> Photos { get; set; }

        public PhotoViewModel()
        {
            Photos = new ObservableCollection<Photo>()
            {
                new Photo("Przykład", "jpg")
            };
        }
    }
}

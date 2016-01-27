using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PhotoManager.Model
{
    public class Photo
    {
        public String Name { get; set; }
        public String FileExtension { get; set; }
        public DateTime DateTaken { get; set; }

        public Photo(String name, String fileExtension)
        {
            Name = name;
            FileExtension = fileExtension;
        }
    }
}

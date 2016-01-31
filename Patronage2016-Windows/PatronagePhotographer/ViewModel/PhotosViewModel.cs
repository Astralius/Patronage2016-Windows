using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Printing;
using Windows.Web.Http.Headers;

using PatronagePhotographer.Model;

namespace PatronagePhotographer.ViewModel
{
    //public class PhotosViewModel : INotifyPropertyChanged
    //{
    //    public ObservableCollection<Photo> Photos { get; set; }
    //    public ICommand SaveCmd { get; set; }
    //    private readonly IPhotoService _photoService;

    //    private int _dateCreated = 0;
    //    public int DateCreated
    //    {
    //        get
    //        {
    //            return _dateCreated;
    //        }
    //        set
    //        {
    //            _dateCreated = value;
    //            OnPropertyChanged("DateCreated");
    //        }
    //    }

    //    public PhotosViewModel(IPhotoService photoService)
    //    {
    //        _photoService = photoService;
    //        SaveCmd = new RelayCommand(pars => Save());
    //        Photos = new ObservableCollection<Photo>();
    //        var photos =_photoService.GetPhotosPaths();
    //        foreach (var photo in photos)
    //        {
    //            Photos.Add(new Photo
    //            {
    //                Name= " aaaaa",
    //                ImageSource = photo
    //            });
    //        }
    //    }

    //    private void Save()
    //    {
    //        // TODO
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged = null;
    //    virtual protected void OnPropertyChanged(string propName)
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, new PropertyChangedEventArgs(propName));
    //    }

    //}
}

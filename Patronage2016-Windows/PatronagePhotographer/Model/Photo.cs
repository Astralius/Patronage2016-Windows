using ExifLib;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace PatronagePhotographer.Model
{
    public class Photo
    {
        public string ImagePath { get; set; }           // = ścieżka do pliku
        public string Name { get; set; }                // = nazwa i rozszerzenie pliku

        // Metadane:
        public double FileSize { get; set; }            // = wielkość pliku (w MB)
        // W tym dane EXIF:
        public int Height { get; set; }
        public int Width { get; set; }
        public DateTime DateTaken { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Photo(IStorageItem file)
        {
            if (file == null) return;
            ImagePath = file.Path;
            Name = Path.GetFileName(ImagePath);
        }

        public void GetPhotoDetails(StorageFile file)
        {
            var task1 = getPhotoBasicPropertiesAsync(file);
            if (!task1.IsCompleted)
                task1.Wait();
            var fileProps = task1.Result;
            if (fileProps != null)
                FileSize = fileProps.Size / 1048576;               // konwersja do MB

            var task2 = getPhotoReadStreamAsync(file);
            if (!task2.IsCompleted)
                task2.Wait();
            var photoStream = task2.Result;

            using (var reader = new ExifReader(photoStream))
            {
                int pictureWidth;
                Width = reader.GetTagValue(ExifTags.PixelXDimension,
                    out pictureWidth) ? pictureWidth : 0;

                int pictureHeight;
                Height = reader.GetTagValue(ExifTags.PixelYDimension,
                    out pictureHeight) ? pictureHeight : 0;

                DateTime pictureTakenDate;
                if (reader.GetTagValue(ExifTags.DateTimeDigitized, out pictureTakenDate))
                    DateTaken = pictureTakenDate;
                else
                    DateTaken = reader.GetTagValue(ExifTags.DateTime,
                        out pictureTakenDate) ? pictureTakenDate : new DateTime(1900, 1, 1);

                double pictureLatitude;
                Latitude = reader.GetTagValue(ExifTags.GPSLatitude,
                    out pictureLatitude) ? pictureLatitude : 0;

                double pictureLongitude;
                Longitude = reader.GetTagValue(ExifTags.GPSLongitude,
                    out pictureLongitude) ? pictureLongitude : 0;
            }
        }

        private async Task<BasicProperties> getPhotoBasicPropertiesAsync(IStorageItem file)
        {
            return await file.GetBasicPropertiesAsync();
        }

        private async Task<Stream> getPhotoReadStreamAsync(IStorageFile file)
        {
            return await file.OpenStreamForReadAsync();
        }
    } // class end

} // namespace end
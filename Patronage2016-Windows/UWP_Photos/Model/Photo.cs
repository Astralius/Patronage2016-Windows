using ExifLib;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace UWP_Photos.Model
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

        public Photo(StorageFile file)
        {
            if (file != null)
            {
                ImagePath = file.Path;
                Name = Path.GetFileName(ImagePath);
            }
        }

        public void GetPhotoDetails(StorageFile file)
        {
            var task1 = getPhotoBasicPropertiesAsync(file);
            if (task1 != null)
            {
                task1.Wait();
                BasicProperties fileProps = task1.Result;
                FileSize = fileProps.Size / 1048576;               // konwersja do MB
            }

            var task2 = getPhotoReadStreamAsync(file);
            if (task2 != null)
            {
                task2.Wait();
                Stream photoStream = task2.Result;

                using (ExifReader reader = new ExifReader(photoStream))                     // wczytuję Metadane (jeśli dostępne)
                {
                    int pictureWidth;
                    if (reader.GetTagValue(ExifTags.PixelXDimension,
                                                out pictureWidth))
                        Width = pictureWidth;
                    else
                        Width = 0;

                    int pictureHeight;
                    if (reader.GetTagValue(ExifTags.PixelYDimension,
                                                out pictureHeight))
                        Height = pictureHeight;
                    else
                        Height = 0;

                    DateTime pictureTakenDate;
                    if (reader.GetTagValue(ExifTags.DateTimeDigitized,
                                                out pictureTakenDate))
                        DateTaken = pictureTakenDate;
                    else
                        if (reader.GetTagValue(ExifTags.DateTime,
                                                out pictureTakenDate))
                        DateTaken = pictureTakenDate;
                    else
                        DateTaken = new DateTime(1900, 1, 1);

                    double pictureLatitude;
                    if (reader.GetTagValue(ExifTags.GPSLatitude,
                                                    out pictureLatitude))
                        Latitude = pictureLatitude;
                    else
                        Latitude = 0;

                    double pictureLongitude;
                    if (reader.GetTagValue(ExifTags.GPSLongitude,
                                                    out pictureLongitude))
                        Longitude = pictureLongitude;
                    else
                        Longitude = 0;
                }
            }
        }

        private async Task<BasicProperties> getPhotoBasicPropertiesAsync(StorageFile file)
        {
            return await file.GetBasicPropertiesAsync();
        }

        private async Task<Stream> getPhotoReadStreamAsync(StorageFile file)
        {
            return await file.OpenStreamForReadAsync();
        }
    } // class end
   
} // namespace end
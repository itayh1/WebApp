using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace WebApplication2
{
    public class PhotosModel
    {      
        public PhotosModel() {

            OutputDir = @"C:\temp\output";// string.Empty;
            Thumbnails = new List<Thumbnail>();
            SetPhotos();
            //ClientConn client = ClientConn.Instance;
        }


        public void SetPhotos() {
            try {

                string dir = OutputDir + "\\Thumbnails";
                if (!Directory.Exists(dir)) {
                    return;
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                string[] extensions = { ".jpg", ".png", ".gif", ".bmp" };
                // run through "year" directories
                foreach (DirectoryInfo yearDir in directoryInfo.GetDirectories()) {
                    // A thumbnalis directory
                    if (!Path.GetDirectoryName(yearDir.FullName).EndsWith("Thumbnails")) {
                        continue;
                    }
                    // run through "month"directories
                    foreach (DirectoryInfo monthDir in yearDir.GetDirectories()) {
                        // run through thumbnails files (pictures)
                        foreach (FileInfo fileInfo in monthDir.GetFiles()) {
                            // has proper extensions
                            if (extensions.Contains(fileInfo.Extension.ToLower())) {

                                Thumbnail pic = this.Thumbnails.Find(x => (x.ThumbnailPath == fileInfo.FullName));
                                if (pic == null) {
                                    Thumbnail newPic = new Thumbnail(fileInfo.FullName);
                                    Thumbnails.Add(newPic);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }


        public void DeletePhoto(string pth) {
            try {
                // run through all thumbnails
                foreach (Thumbnail photo in Thumbnails) {
                    // photo is in thumbnails
                    if (photo.PhotoPath.Equals(pth)) {
                        File.Delete(photo.PhotoPath);
                        File.Delete(photo.ThumbnailPath);
                        Thumbnails.Remove(photo);
                        break;
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumbnails")]
        public List<Thumbnail> Thumbnails;


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputDir")]
        public string OutputDir;
    }
}
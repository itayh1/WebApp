using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2
{
    public class Thumbnail
    {
        // counter for all pictures
        public static int count = 0;

        public Thumbnail(string path) {
            
            count++;
            try
            {
                // creation of members pathes
                ThumbnailPath = path;
                PhotoPath = path.Replace(@"Thumbnails\", string.Empty);
                Name = Path.GetFileNameWithoutExtension(ThumbnailPath);
                Month = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(ThumbnailPath));
                Year = Path.GetFileNameWithoutExtension(Path.GetDirectoryName((Path.GetDirectoryName(ThumbnailPath))));

                // creation of semi pathes from the original path
                int semiPathLength = ThumbnailPath.Length;
                int startSemiPath = ThumbnailPath.IndexOf("output");
                string dir = ThumbnailPath.Substring(startSemiPath, semiPathLength - startSemiPath);
                ThumbnailSemiPath = @"~\" + dir;
                PhotoSemiPath = ThumbnailSemiPath.Replace(@"Thumbnails\", string.Empty);

            } catch(Exception ex) {

                Console.WriteLine(ex.Message);
            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailPath")]
        public string ThumbnailPath { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSemiPath")]
        public string ThumbnailSemiPath { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoPath")]
        public string PhotoPath { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoSemiPath")]
        public string PhotoSemiPath { get; set; }

    }
}
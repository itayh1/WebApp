using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class ConfigModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "outputDir")]
        public string outputDir;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "logName")]
        public string logName;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "sourceName")]
        public string sourceName;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "thumbnailSize")]
        public string thumbnailSize;

        [Display(Name = "handlers")]
        public List<string> handlers = new List<string> { "h1", "h2", "h3"};
    }
}
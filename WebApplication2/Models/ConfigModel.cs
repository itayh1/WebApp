using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class ConfigModel
    {
        public ConfigModel()
        {

        }

        [Display(Name = "outputDir")]
        public string outputDir;

        [Display(Name = "logName")]
        public string logName;

        [Display(Name = "sourceName")]
        public string sourceName;

        [Display(Name = "thumbnailSize")]
        public string thumbnailSize;

        public List<string> handlers;

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class LogObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="msg">message</param>
        public LogObject(string type, string msg)
        {
            Type = type.ToString();
            Messgae = msg;
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Messgae")]
        public string Messgae { get; set; }
    }
}
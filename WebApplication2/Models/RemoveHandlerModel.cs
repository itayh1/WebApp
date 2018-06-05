using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class RemoveHandlerModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "handler")]
        public string handler { get; set; }
    }
}
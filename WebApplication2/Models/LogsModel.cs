using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class LogsModel
    {
        public LogsModel()
        {

        }

        [Display(Name = "logs")]
        public List<LogObject> logs;
    }
}
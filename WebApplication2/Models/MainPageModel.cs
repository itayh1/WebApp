using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class MainPageModel
    {
        public MainPageModel()
        {
            this.isConnected = "No";
            this.numOfImages = 0;
        }

        [Display(Name = "students")]
        public List<Student> students = new List<Student>()
        {
         new Student {ID = 209127596, FirstName = "Itay", LastName = "Hasid"},
         new Student {ID = 200876548, FirstName = "Omer", LastName = "Zucker"}
        };

        [Display(Name = "numOfImages")]
        public int numOfImages;

        [Display(Name = "isConnected")]
        public string isConnected;
    }
}
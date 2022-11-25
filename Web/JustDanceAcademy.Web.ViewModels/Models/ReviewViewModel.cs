using JustDanceAcademy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class ReviewViewModel
    {

        public int Id
        {
            get; set;
        }

        public int ClassId
        {
            get; set;
        }

        public string NameClass
        {
            get; set;
        }

        public string Student
        {
            get; set;
        }

        public string Context
        {
            get; set;
        }




    }
}

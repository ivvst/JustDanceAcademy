using JustDanceAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class MyClassViewModel 
    {

        public string StudentName
        {
            get; set;
        }
        public int Id
        {
            get; set;
        }

        public string Name { get; set; } = null!;

        public string Instructor { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }
    }
}

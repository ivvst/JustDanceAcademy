using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class AddScheduleViewModel
    {

        public DateTime StartClass
        {
            get; set;
        }

        public DateTime EndClass
        {
            get; set;
        }

        public Day Day
        {
            get; set;
        }


        public Age Age
        {
            get; set;
        }

        [Required]
        [Display(Name = "Choose class")]
        public int ClassId
        {
            get; set;
        }

        public string LevelCategory
        {
            get; set;
        }
        public IEnumerable<Class> AllClasses { get; set; } = new List<Class>();
    }
}

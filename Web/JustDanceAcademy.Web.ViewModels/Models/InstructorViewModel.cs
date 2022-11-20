using JustDanceAcademy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class InstructorViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} its too short", MinimumLength = 3 )]

        public string FullName
        {
            get; set;
        }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} its too short", MinimumLength = 5)]

        public string ImageUrl
        {
            get; set;
        }


        [Required]
        [StringLength(200,ErrorMessage ="The {0} its too short", MinimumLength =5)]
        public string AboutYou
        {
            get; set;
        }

        [Required]
        [Display(Name = "Choose class")]
        public int ClassId
        {
            get; set;
        }

        public IEnumerable<Class> ClassesOfInstructor { get; set; } = new List<Class>();
    }
}

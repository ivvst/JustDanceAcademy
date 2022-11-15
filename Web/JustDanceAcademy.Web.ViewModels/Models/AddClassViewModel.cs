using JustDanceAcademy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class AddClassViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Name
        {
            get; set;
        }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Instructor
        {
            get; set;
        }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl
        {
            get; set;
        }


        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Description
        {
            get; set;
        }
        [Display(Name = "Level of dancing")]
        public int LevelCategoryId
        {
            get; set;
        }
        public IEnumerable<LevelCategory> LevelsCategory { get; set; } = new List<LevelCategory>();

    }
}

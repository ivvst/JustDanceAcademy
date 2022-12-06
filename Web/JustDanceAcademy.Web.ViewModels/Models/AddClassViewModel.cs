﻿using JustDanceAcademy.Data.Models;
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
        public int Id
        {
            get; set;
        }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Name
        {
            get; set;
        }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string? Instructor
        {
            get; set;
        }

        [Required]
        [Display(Name = "Image URL")]
        public string? ImageUrl
        {
            get; set;
        }


        [Required]
        [StringLength(100,MinimumLength =5)]
        public string? Description
        {
            get; set;
        }

        [Required]
        [Display(Name = "Level of dancing")]
        public int LevelCategoryId
        {
            get; set;
        }
        public LevelCategory LevelCategoryAdd
        {
            get; set;
        }
        public IEnumerable<LevelCategory> LevelsCategory { get; set; } = new List<LevelCategory>();

    }
}


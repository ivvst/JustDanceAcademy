﻿using JustDanceAcademy.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Data.Models
{
    public class Instrustor : BaseDeletableModel<int>
    {
        [Key]
        public int Id
        {
            get; set;
        } 

        [Required]
        [StringLength(100)]
        public string Name
        {
            get; set;

        }

        [Required]
        [StringLength(700)]
        public string Biography
        {
            get; set;
        }

        
        public string? ImageUrl
        {
            get; set;
        }

        public int ClassId
        {
            get; set;
        }
        public Class Class
        {
            get; set;
        }

    }
}




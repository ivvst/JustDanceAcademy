using JustDanceAcademy.Data.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JustDanceAcademy.Data.Models
{
    public class Class : BaseDeletableModel<int>
    {

        public Class()
        {
            this.Students = new HashSet<ClassStudent>();
            this.Reviews = new HashSet<Review>();
        }

        [Key]
        [Required]
        public new int Id
        {
            get; set;
        }

        [Required]
        public string Name
        {
            get; set;
        }


        public string ImageUrl
        {
            get; set;
        } = null!;

        [Required]
        public string Description
        {
            get; set;
        }


        [Required]
        public string Instructor
        {
            get; set;
        }

        public int LevelCategoryId
        {
            get; set;
        }

        [ForeignKey(nameof(LevelCategoryId))]
        public LevelCategory LevelCategory
        {
            get; set;
        }

  

        public virtual ICollection<ClassStudent> Students
        {
            get; set;
        }

        public virtual ICollection<Review> Reviews
        {
            get; set;
        }

    }
}


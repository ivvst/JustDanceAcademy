using JustDanceAcademy.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Data.Models
{
    public class ClassStudent:BaseDeletableModel<int>
    {
        public int ClassId
        {
            get; set;
        }

        public  virtual Class Class
        {
            get; set;
        }

        public virtual ApplicationUser Student
        {
            get; set;
        }

        [ForeignKey(nameof(StudentId))]
        public string StudentId
        {

            get; set;
        }


    }
}

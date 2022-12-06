namespace JustDanceAcademy.Web.ViewModels.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using JustDanceAcademy.Data.Models;
    using JustDanceAcademy.Data.Models.Enum;

    public class AddScheduleViewModel
    {
        public int Id
        {
            get; set;
        }

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


        public Class Class
        {
            get; set;
        }
        public IEnumerable<Class> AllClasses { get; set; } = new List<Class>();
    }
}

namespace JustDanceAcademy.Web.ViewModels.Models
{
    using Microsoft.VisualBasic;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ScheduleViewModel
    {
		public int Id
		{
			get; set;
		}

		[Required]

		public string Day
        {
            get; set;
        }

		public DateTime Start
        {
            get; set;
        }

		public DateTime End
        {
            get; set;
        }

		public string Class
        {
            get; set;
        }

		public string Age
        {
            get; set;
        }

		public string LevelCategory
        {
            get; set;
        }
    }
}

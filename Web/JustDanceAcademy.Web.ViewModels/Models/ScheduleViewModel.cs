namespace JustDanceAcademy.Web.ViewModels.Models
{
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

		public string Start
        {
            get; set;
        }

		public string End
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

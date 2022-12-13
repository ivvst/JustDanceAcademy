namespace JustDanceAcademy.Web.ViewModels.Models
{
	using System.ComponentModel.DataAnnotations;

	public class ReviewViewModel
	{
		public int Id
		{
			get; set;
		}

		public int ClassId
		{
			get; set;
		}

		public string NameClass
		{
			get; set;
		}

		public string Student
		{
			get; set;
		}

		[Required]
		[StringLength(200, ErrorMessage = "The {0} must be at least with {1}", MinimumLength = 10)]
		public string Context
		{
			get; set;
		}
	}
}

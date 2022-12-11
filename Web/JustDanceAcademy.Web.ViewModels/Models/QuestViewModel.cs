namespace JustDanceAcademy.Web.ViewModels.Models
{
	using System.ComponentModel.DataAnnotations;

	public class QuestViewModel
	{
		public int Id
		{
			get; set;
		}

		[Required]
		[StringLength(100, ErrorMessage = "The {0} should contains {2} symbols minimum.", MinimumLength = 7)]
		public string Question
		{
			get; set;
		}

		[Required]
		[StringLength(150, ErrorMessage = "The {0} should contains {2} symbols minimum.", MinimumLength = 3)]
		public string Answer
		{
			get; set;
		}
	}
}

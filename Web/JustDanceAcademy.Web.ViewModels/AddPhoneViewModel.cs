namespace JustDanceAcademy.Web.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	public class AddPhoneViewModel
	{
		[Required]
		[Phone]
		public int PhoneNumber
		{
			get; set;
		}
	}
}

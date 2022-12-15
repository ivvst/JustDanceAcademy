namespace JustDanceAcademy.Web.ViewModels.Models
{
	using JustDanceAcademy.Data.Models.Enum;

	public class MyClassViewModel
	{
		public string StudentName
		{
			get; set;
		}

		public int Id
		{
			get; set;
		}

		public string Name { get; set; } = null!;

		public string Instructor { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public string Description
		{
			get; set;
		}

		public string Category
		{
			get; set;
		}

		public decimal PlanPrice
		{
			get; set;
		}

		public Age AgeType
		{
			get; set;
		}
	}
}

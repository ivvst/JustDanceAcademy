namespace JustDanceAcademy.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	using JustDanceAcademy.Data.Common.Models;

	public class LevelCategory : BaseDeletableModel<int>
	{
		[Key]
		public int Id
		{
			get; set;
		}

		[Required]
		[StringLength(50)]
		public string Name
		{
			get; set;
		}
	}
}

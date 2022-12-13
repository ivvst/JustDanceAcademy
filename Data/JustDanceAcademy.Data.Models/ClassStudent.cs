namespace JustDanceAcademy.Data.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	using JustDanceAcademy.Data.Common.Models;

	public class ClassStudent : BaseDeletableModel<int>
	{
		public int ClassId
		{
			get; set;
		}

		public virtual Class Class
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

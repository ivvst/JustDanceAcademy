namespace JustDanceAcademy.Data.Models
{
	using JustDanceAcademy.Data.Common.Models;

	public class TestStudent : BaseDeletableModel<int>
	{
		public MemberShip Plan
		{
			get; set;
		}

		public int PlanId
		{
			get; set;
		}

		public ApplicationUser Student
		{
			get; set;
		}

		public string StudentId
		{
			get; set;
		}
	}
}

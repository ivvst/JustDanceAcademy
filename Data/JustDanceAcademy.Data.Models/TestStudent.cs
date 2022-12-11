using JustDanceAcademy.Data.Common.Models;
using System.Runtime.InteropServices;

namespace JustDanceAcademy.Data.Models
{
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

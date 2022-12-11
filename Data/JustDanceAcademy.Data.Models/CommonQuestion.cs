namespace JustDanceAcademy.Data.Models
{
	using JustDanceAcademy.Data.Common.Models;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class CommonQuestion: BaseDeletableModel<int>
	{
		public int Id
		{
			get; set;
		}

		public string Question
		{
			get; set;
		}

		public string Answear
		{
			get; set;
		}
	}
}

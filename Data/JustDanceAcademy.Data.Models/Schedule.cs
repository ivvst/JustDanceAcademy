namespace JustDanceAcademy.Data.Models
{
	using System;

	using JustDanceAcademy.Data.Common.Models;
	using JustDanceAcademy.Data.Models.Enum;

	public class Schedule : BaseDeletableModel<int>
	{
		public int Id
		{
			get; set;
		}

		// CLASS NAME
		public int ClassId
		{
			get; set;
		}

		public Class Class
		{
			get; set;
		}

		public DateTime StartTime
		{
			get; set;
		}

		public DateTime EndTime
		{
			get; set;
		}

		public Day Day
		{
			get; set;
		}

		public Age Age
		{
			get; set;
		}
	}
}

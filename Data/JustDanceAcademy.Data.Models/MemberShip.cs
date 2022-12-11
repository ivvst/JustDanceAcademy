namespace JustDanceAcademy.Data.Models
{
	using JustDanceAcademy.Data.Common.Models;
	using JustDanceAcademy.Data.Models.Enum;
	using Microsoft.EntityFrameworkCore.Metadata.Internal;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class MemberShip : BaseDeletableModel<int>
	{

		public MemberShip()
		{
			this.Students = new HashSet<TestStudent>();
		}


		public int Id
		{
			get; set;
		}

		// MONTH
		public string Title
		{
			get; set;
		}

		public Age Age
		{
			get; set;
		}

		[Column(TypeName = "decimal(18,0)")]
		public decimal Price
		{
			get; set;
		}

		public string DetailOne
		{
			get; set;
		}

		public string DetailTwo
		{
			get; set;
		}

		public string DetailThree
		{
			get; set;
		}

		public virtual ICollection<TestStudent> Students
		{
			get; set;
		}


	}
}

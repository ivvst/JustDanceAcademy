using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels
{
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

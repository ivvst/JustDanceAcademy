namespace JustDanceAcademy.Web.ViewModels.Administration.Users
{
	using System.Collections.Generic;

	public class AllUsersViewModel
	{

		public IEnumerable<UserListViewModel> Users
		{
			get; set;
		}
	}
}

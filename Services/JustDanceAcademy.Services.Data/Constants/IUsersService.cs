namespace JustDanceAcademy.Services.Data.Constants
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using JustDanceAcademy.Web.ViewModels.Administration.Users;

	public interface IUsersService
	{
		public Task<IEnumerable<UserListViewModel>> GetAllUsersAsync();
	}
}

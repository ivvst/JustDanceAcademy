using JustDanceAcademy.Web.ViewModels.Administration.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data.Constants
{
	public interface IUsersService
	{
		public Task<IEnumerable<UserListViewModel>> GetAllUsersAsync();
	}
}

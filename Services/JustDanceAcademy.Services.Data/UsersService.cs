namespace JustDanceAcademy.Services.Data
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Services.Mapping;
	using JustDanceAcademy.Web.ViewModels.Administration.Users;
	using Microsoft.EntityFrameworkCore;

	public class UsersService : IUsersService
	{
		private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

		public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<IEnumerable<UserListViewModel>> GetAllUsersAsync()
		{
			return await this.userRepository.All().Select(x => new UserListViewModel()
			{
				Id = x.Id,
				UserName = x.UserName,
				Class = x.Class.Name,
			}).ToListAsync();
		}
	}
}

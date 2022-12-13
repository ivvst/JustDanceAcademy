namespace JustDanceAcademy.Services.Data
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Constants;
	using Microsoft.EntityFrameworkCore;

	public class LevelDanceService : ILevelCategoryService
	{
		private readonly IRepository<LevelCategory> levelRepo;

		public LevelDanceService(IRepository<LevelCategory> levelRepo)
		{
			this.levelRepo = levelRepo;
		}

		public async Task<IEnumerable<LevelCategory>> AllCategories()
		{
			return await this.levelRepo.AllAsNoTracking().ToListAsync();
		}

		public async Task<string> DoesNameOfDanceCategoryExist(string name)
		{
			var result = await this.levelRepo.AllAsNoTracking().Where(x => x.Name == name).FirstOrDefaultAsync();

			if (result == null)
			{
				return string.Empty;
			}

			return result.Name;
		}
	}
}

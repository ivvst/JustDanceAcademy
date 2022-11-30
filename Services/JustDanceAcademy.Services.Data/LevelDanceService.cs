namespace JustDanceAcademy.Services.Data
{
    using System.Collections.Generic;
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

        // public async Task<IEnumerable<LevelDanceViewModel>> AllCategories()
        // {
        //    return await this.levelRepo.All().OrderBy(x => x.Name).Select(l => new LevelDanceViewModel()
        //    {
        //        Id = l.Id,
        //        Name = l.Name,
        //    })
        //        .ToListAsync();
        // }
    }
}

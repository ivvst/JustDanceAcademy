using JustDanceAcademy.Data.Common.Models;
using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Services.Mapping;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data
{
    public class LevelDanceService : ILevelCategoryService
    {
        private readonly IRepository<LevelCategory> levelRepo;

        public LevelDanceService(IRepository<LevelCategory> levelRepo)
        {
            this.levelRepo = levelRepo;
        }

        public async Task<IEnumerable<LevelCategory>> AllCategories()
        {
            return await this.levelRepo.All().ToListAsync();
        }

        //public async Task<IEnumerable<LevelDanceViewModel>> AllCategories()
        //{
        //    return await this.levelRepo.All().OrderBy(x => x.Name).Select(l => new LevelDanceViewModel()
        //    {
        //        Id = l.Id,
        //        Name = l.Name,
        //    })
        //        .ToListAsync();
        //}


    }
}

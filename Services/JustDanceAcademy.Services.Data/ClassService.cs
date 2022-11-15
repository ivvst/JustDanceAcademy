using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data.Constants;
//using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data
{
    public class ClassService : IDanceClassService
    {
        private readonly IRepository<Class> classRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        

        public ClassService(IRepository<Class> classRepository, IRepository<ApplicationUser> userRepository)
        {
            this.classRepository = classRepository;
            this.userRepository = userRepository;

        }
     
        public async Task<int> CreateClassAsync(AddClassViewModel model)
        {

            var entity = new Class()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Instructor = model.Instructor,
                LevelCategoryId=model.LevelCategoryId,
                Description = model.Description,

            };
            await this.classRepository.AddAsync(entity);
            await this.classRepository.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<IEnumerable<ClassesViewModel>> GetAllAsync()
        {
            return await this.classRepository.All()
                .OrderBy(c => c.Name)
                .Select(c => new ClassesViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                Description = c.Description,
                Instructor = c.Instructor,
                Category=c.LevelCategory.Name,


            })
                .ToListAsync();
        }
    }
}

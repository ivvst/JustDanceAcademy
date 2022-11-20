using DanceAcademy.Models;
using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data.Constants;
//using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace JustDanceAcademy.Services.Data
{
    public class ClassService : IDanceClassService
    {
        private readonly IRepository<Class> classRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<MemberShip> planRepo;
        private readonly IRepository<LevelCategory> levelRepo;

    

        public ClassService(IRepository<Class> classRepository, IRepository<ApplicationUser> userRepository, IRepository<MemberShip> planRepo, IRepository<LevelCategory> levelRepo)
        {
            this.classRepository = classRepository;
            this.userRepository = userRepository;
            this.planRepo = planRepo;
            this.levelRepo = levelRepo;


        }

        public async Task<int> CreateClassAsync(AddClassViewModel model)
        {

            var entity = new Class()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Instructor = model.Instructor,
                LevelCategoryId = model.LevelCategoryId,
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
                    Category = c.LevelCategory.Name,


                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PlanViewModel>> GetAllPlans()
        {
            return await this.planRepo.All()
                .Select(p => new PlanViewModel()
                {
                    Title = p.Title,
                    Price = p.Price,
                })
                .ToListAsync();
        }
        public async Task<int> CreatePlan(PlanViewModel model)
        {

            var plan = new MemberShip()
            {
                Title = model.Title,
                Price = model.Price,
            };
            await this.planRepo.AddAsync(plan);
            await this.planRepo.SaveChangesAsync();

            return plan.Id;
        }

        public async Task<ClassQueryModel> All(string category = null, string searchTerm = null, int currentPage = 1, int classPerPage = 1)
        {
            var result = new ClassQueryModel();
            var classes = this.classRepository.All();

            if (string.IsNullOrEmpty(category) == false)
            {
                classes = classes
                    .Where(c => c.LevelCategory.Name == category);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                classes = classes
             .Where(c => EF.Functions.Like(c.Name.ToLower(), searchTerm) ||
             EF.Functions.Like(c.Instructor.ToLower(), searchTerm));
            }

            //ADD sorting switch in order to  get that below
            result.Classes = await classes
                .Skip((currentPage - 1) * classPerPage)
                .Take(classPerPage)
                .Select(c => new ClassServiceViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    Instructor = c.Instructor,
                })
                .ToListAsync();

            result.TotalClassesCount = await classes.CountAsync();

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await this.levelRepo.All()
                 .Select(c => c.Name)
                 .Distinct()
                 .ToListAsync();
        }
    }
}

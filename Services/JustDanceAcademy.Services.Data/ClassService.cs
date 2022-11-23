using DanceAcademy.Models;
using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data.Common;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Services.Messaging.Constants;
//using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
        private readonly IRepository<ClassStudent> comboRepo;





        public ClassService(IRepository<ClassStudent> comboRepo, IRepository<Class> classRepository, IRepository<ApplicationUser> userRepository, IRepository<MemberShip> planRepo, IRepository<LevelCategory> levelRepo)
        {
            this.classRepository = classRepository;
            this.userRepository = userRepository;
            this.planRepo = planRepo;
            this.levelRepo = levelRepo;
            this.comboRepo = comboRepo;


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

        public async Task AddStudentToClass(string userId, int classId)
        {
            var student = await this.userRepository.All()
          .FirstOrDefaultAsync(s => s.Id == userId);

            if (student == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            if (student.ClassId.HasValue == false)
            {
                var danceClass = await this.classRepository.All()
                    .Where(c => c.Id == classId)
                    .FirstAsync();

                student.ClassId = classId;
                danceClass.Students.Add(new ClassStudent()
                {
                    StudentId = student.Id,
                    ClassId = classId,
                    Class = danceClass,
                    Student = student,
                });
                await this.classRepository.SaveChangesAsync();
                await this.userRepository.SaveChangesAsync();
            }
            else if (student.ClassId.HasValue)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ClassAlreadyIsStarted, classId));

            }



        }

        public async Task LeaveClass(int classId, string userId)
        {
            var student = await this.userRepository.All()
                .Where(u => u.Id == userId)
                .Include(x => x.Class.Students)
                .FirstAsync();


            var st = student.Class.Students.First(x => x.StudentId == userId).IsDeleted = true;
            //var item = student.Class.Students.First(x => x.ClassId == classId).IsDeleted = true;


            student.ClassId = null;





            await this.comboRepo.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();



        }


        public async Task<IEnumerable<MyClassViewModel>> GetMyClassAsync(string userId)
        {
            var user = await userRepository.All()
                .Where(u => u.Id == userId)
                .Include(x => x.Class.Students)
                .FirstAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            if (user.ClassId.HasValue)
            {

                return user.Class.Students
                    .Select(x => new MyClassViewModel()
                    {
                        Id = x.Class.Id,
                        Name = x.Class.Name,
                        ImageUrl = x.Class.ImageUrl,
                        Instructor = x.Class.Instructor,
                        Description = x.Class.Description,
                        Category = x.Class.LevelCategory?.Name,
                    });
            }
            return null;


        }

        public async Task Edit(int classId, EditDanceViewModel model)
        {
            var dance = await this.classRepository.All().Where(d => d.Id == classId).FirstAsync();
            if (dance == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound, classId));
            }

            var danceCategory = await this.levelRepo.All().AnyAsync(x => x.Id == dance.LevelCategoryId);

            if (danceCategory == false)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.InvalidDanceCategoryType, dance.LevelCategory.Name));
            }

           

            dance.Description = model.Description;
            dance.ImageUrl = model.ImageUrl;
            dance.Instructor = model.Instructor;
            dance.Name = model.Name;
            dance.LevelCategoryId = model.LevelCategoryId;

            await this.classRepository.SaveChangesAsync();
        }



        public async Task<int> GetDanceLevelId(int classId)
        {
            return await this.classRepository.All().Where(x => x.Id == classId)
                .Select(x => x.LevelCategoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await classRepository.All().AnyAsync(x => x.Id == id);
        }

        public async Task<ClassesViewModel> DanceDetailsById(int id)
        {
            return await classRepository.All()
                 .Where(c => c.Id == id)
                 .Select(c => new ClassesViewModel()
                 {
                     Name = c.Name,
                     Category = c.LevelCategory.Name,
                     Id = id,
                     ImageUrl = c.ImageUrl,
                     Description = c.Description,
                     Instructor = c.Instructor,
                 })
                 .FirstAsync();
        }
    }
}





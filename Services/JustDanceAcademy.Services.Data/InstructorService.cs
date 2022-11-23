using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data.Common;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data
{
    public class InstructorService : IServiceInstructor
    {
        private readonly IRepository<Instrustor> repo;
        private readonly IRepository<Class> classRepository;



        public InstructorService(IRepository<Instrustor> repo, IRepository<Class> classRepository)
        {
            this.classRepository = classRepository;

            this.repo = repo;
        }

        public async Task<int> AddInstructor(InstructorViewModel model)

        {
            var entity = new Instrustor()
            {
                Name = model.FullName,
                ImageUrl = model.ImageUrl,
                Biography = model.AboutYou,
                ClassId = model.ClassId,
            };

            bool doesInstructorExist = await this.repo.All().AnyAsync(x => x.Name == model.FullName);
            if (doesInstructorExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.InstructorAlreadyExists, model.FullName));
            }

            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Edit(int trainerId, InstructorViewModel model)
        {
            var trainer = await this.repo.All()
                .Where(x => x.Id == trainerId)
                .FirstAsync();
            if (trainer == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.InstructorNotFound, trainerId));
            }

            var danceClass = await this.classRepository.All().AnyAsync(x => x.Id == trainer.ClassId);

            if (danceClass == false)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound, trainer.ClassId));
            }

            trainer.Biography = model.AboutYou;
            trainer.ImageUrl = model.ImageUrl;
            trainer.Name = model.FullName;
            trainer.ClassId = model.ClassId;

            await this.repo.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await this.repo.All().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<InstructorsViewModel>> GetAllInstructors()
        {
            return await this.repo.All().Select(i => new InstructorsViewModel()
            {
                Id = i.Id,
                FullName = i.Name,
                ImageUrl = i.ImageUrl,
                Class = i.Class.Name,
                Intro = i.Biography,
            })
                .ToListAsync();
        }
        //public async Task<IEnumerable<ClassesViewModel>> GetAllAsync()
        //{
        //    return await this.classRepository.All()
        //        .OrderBy(c => c.Name)
        //        .Select(c => new ClassesViewModel()
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            ImageUrl = c.ImageUrl,
        //            Description = c.Description,
        //            Instructor = c.Instructor,
        //            Category = c.LevelCategory.Name,


        //        })
        //        .ToListAsync();
        //}
        public async Task<IEnumerable<Class>> GetClasses()
        {
            return await classRepository.All().ToListAsync();
        }

        public async Task<int> GetClassId(int trainerId)
        {
            return await this.repo.All().Where(x => x.Id == trainerId)
                .Select(x => x.ClassId)
                .FirstOrDefaultAsync();
        }

        public async Task<InstructorsViewModel> TrainerDetailsById(int id)
        {
            return await this.repo.All().Where(t => t.Id == id)
                 .Select(t => new InstructorsViewModel()
                 {
                     Intro = t.Biography,
                     Id = id,
                     ImageUrl = t.ImageUrl,
                     FullName = t.Name,
                     Class = t.Class.Name,
                 })
                 .FirstAsync();
        }
    }
}

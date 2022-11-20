using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
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
            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();

            return entity.Id;
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





    }
}

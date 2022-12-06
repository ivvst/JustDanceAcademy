namespace JustDanceAcademy.Services.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.EntityFrameworkCore;

	public class InstructorService : IServiceInstructor
	{
		private readonly IRepository<Instrustor> repo;
		private readonly IRepository<Class> classRepository;
		




		public InstructorService(IRepository<Instrustor> repo, IRepository<Class> classRepository)
		{
			this.classRepository = classRepository;

			this.repo = repo;
			
		}

		public async Task<bool> DoesInstructorExist(string name)
		{
			var result = await this.repo.AllAsNoTracking().Where(x => x.Name == name).FirstOrDefaultAsync();

			if (result != default)
			{
				return true;
			}
			return false;
		}


		public async Task<int> AddInstructor(InstructorViewModel model)

		{
			var entity = new Instrustor()
			{
				Id = model.Id,
				Name = model.FullName,
				ImageUrl = model.ImageUrl,
				Biography = model.AboutYou,
				ClassId = model.ClassId,
				Class = model.Class,
			};

			await this.repo.AddAsync(entity);
			await this.repo.SaveChangesAsync();

			return entity.Id;
		}


		public async Task Edit(int trainerId, InstructorViewModel model)
		{
			var trainer = await this.repo.All()
				.Where(x => x.Id == trainerId)
				.FirstOrDefaultAsync();
			if (trainer == null)
			{
				throw new NullReferenceException(string.Format(ExceptionMessages.InstructorNotFound));
			}

			trainer.Biography = model.AboutYou;
			trainer.ImageUrl = model.ImageUrl;
			trainer.Name = model.FullName;
			trainer.ClassId = model.ClassId;

			var danceClass = await this.classRepository.All().FirstOrDefaultAsync(x => x.Id == model.ClassId);

			if (danceClass == null)
			{
				throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound));
			}
			await this.repo.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await this.repo.All().AnyAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<Instrustor>> GetAllInstructors()
		{
			return await this.repo.AllAsNoTracking().Include(x => x.Class).ThenInclude(x => x.LevelCategory).ToListAsync();
			//return await this.repo.All().Select(i => new InstructorsViewModel()
			//{
			//	Id = i.Id,
			//	FullName = i.Name,
			//	ImageUrl = i.ImageUrl,
			//	Class = i.Class.Name,
			//	Intro = i.Biography,
			//})
			//	.ToListAsync();
		}

		public async Task<IEnumerable<Class>> GetClasses()
		{
			return await this.classRepository.AllAsNoTracking().Include(x => x.LevelCategory).ToListAsync();
		}

		public async Task<int> GetClassId(int trainerId)
		{
			return await this.repo.AllAsNoTracking().Where(x => x.Id == trainerId)
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

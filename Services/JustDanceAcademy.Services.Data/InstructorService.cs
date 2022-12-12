namespace JustDanceAcademy.Services.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
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

			//var danceClass = await this.classRepository.All().FirstOrDefaultAsync(x => x.Id == model.ClassId);

			//if (danceClass == null)
			//{
			//	throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound));
			//}
			await this.repo.SaveChangesAsync();
		}

		public async Task<bool> Exists(int id)
		{
			return await this.repo.All().AnyAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<Instrustor>> GetAllInstructors()
		{
			return await this.repo.AllAsNoTracking().Include(x => x.Class).ThenInclude(x => x.LevelCategory).ToListAsync();
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

		public async Task<Instrustor> DeleteInstructor(int trainerId)
		{
			var trainerName = await this.repo.All().Where(x => x.Id == trainerId).Select(x => x.Name).FirstOrDefaultAsync();

			var dance = await this.classRepository.All().AnyAsync(x => x.Instructor == trainerName);

			if (dance == true)
			{
				throw new ArgumentException();
			}

			var trainer = await this.repo.All().FirstOrDefaultAsync(x => x.Id == trainerId);
			trainer.IsDeleted = true;
			trainer.DeletedOn = DateTime.Now;

			this.repo.Update(trainer);
			await this.repo.SaveChangesAsync();

			return trainer;
		}

		public async Task<Dictionary<string, List<string>>> GetClassWithAllCategoriesView(int classId)
		{
			var result = await this.classRepository.AllAsNoTracking().Include(x => x.LevelCategory).ToListAsync();
			var list = new Dictionary<string, List<string>>();

			var danceClass = await this.classRepository.All().FirstOrDefaultAsync(x => x.Id == classId);

			foreach (var dance in result)
			{
				if (danceClass.Name == dance.Name)
				{
					if (list.ContainsKey(dance.Name))
					{
						list[dance.Name].Add(dance.LevelCategory.Name);
					}
					else
					{
						list.Add(dance.Name, new List<string>());
						list[dance.Name].Add(dance.LevelCategory.Name);
					}
				}
			}

			return list;

		}
	}
}

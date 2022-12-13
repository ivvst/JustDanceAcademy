namespace JustDanceAcademy.Services.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.EntityFrameworkCore;

	public class ScheduleService : IScheduleService
	{
		private readonly IRepository<Class> classRepository;
		private readonly IRepository<Schedule> scheduleRepo;
		private readonly IRepository<LevelCategory> levelRepo;

		public ScheduleService(IRepository<Schedule> scheduleRepo, IRepository<Class> classRepository, IRepository<LevelCategory> levelRepo)
		{
			this.scheduleRepo = scheduleRepo;
			this.classRepository = classRepository;
			this.levelRepo = levelRepo;
		}

		public async Task<IEnumerable<Schedule>> AllSchedules()
		{
			return await this.scheduleRepo.AllAsNoTracking().Include(x => x.Class).ThenInclude(x => x.LevelCategory).ToListAsync();
		}

		public async Task<int> CreateSchedule(AddScheduleViewModel model)
		{
			var entity = new Schedule()
			{
				Id = model.Id,
				StartTime = model.StartClass,
				EndTime = model.EndClass,
				Day = model.Day,
				Age = model.Age,
				ClassId = model.ClassId,
				Class = model.Class,
			};
			if (model.StartClass > model.EndClass)
			{
				throw new NullReferenceException();
			}

			await this.scheduleRepo.AddAsync(entity);
			await this.scheduleRepo.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<Schedule> DeleteColumn(int test)
		{
			var item = await this.scheduleRepo.All().FirstOrDefaultAsync(x => x.Id == test);

			if (item != null)
			{
				item.IsDeleted = true;
				item.DeletedOn = DateTime.UtcNow;
				this.scheduleRepo.Update(item);
			}
			else
			{
				throw new NullReferenceException();
			}

			await this.scheduleRepo.SaveChangesAsync();
			return item;
		}

		public async Task<IEnumerable<Class>> GetClasses()
		{
			return await this.classRepository.All().Include(x => x.LevelCategory).ToListAsync();
		}

		public async Task<int> GetScheduleById(int id)
		{
			var schedule = await this.scheduleRepo.All().Where(x => x.Id == id).FirstAsync();

			if (schedule != null)
			{
				return schedule.Id;
			}

			return 0;
		}
	}
}

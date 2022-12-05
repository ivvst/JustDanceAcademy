using JustDanceAcademy.Data.Common.Repositories;
using JustDanceAcademy.Data.Models;
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

		public async Task<IEnumerable<ScheduleViewModel>> AllSchedules()
		{
			return await this.scheduleRepo.All()
				.OrderBy(x => x.Day)
				.Select(s => new ScheduleViewModel()
				{
					Id = s.Id,
					Start = s.StartTime.ToShortTimeString(),
					End = s.EndTime.ToShortTimeString(),
					LevelCategory = s.LevelCategory,
					Age = s.Age.ToString(),
					Class = s.Class.Name,
					Day = s.Day.ToString(),



				})
				.ToListAsync();
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
				LevelCategory = model.LevelCategory,
			};
			var existCategory = await this.levelRepo.All().Where(x => x.Name == model.LevelCategory).FirstOrDefaultAsync();
			if (existCategory != null && model.StartClass < model.EndClass)
			{
				if (model.StartClass < model.EndClass)
				{

					await this.scheduleRepo.AddAsync(entity);
					await this.scheduleRepo.SaveChangesAsync();
					return entity.Id;
				}
			}


			throw new NullReferenceException(string.Format(ExceptionMessages.InvalidDanceCategoryType));
		}

		public async Task<Schedule> DeleteColumn(int test)
		{
			var item = await scheduleRepo.All().FirstOrDefaultAsync(x => x.Id == test);

			if (item != null)
			{
				item.IsDeleted = true;
				item.DeletedOn = DateTime.UtcNow;
				this.scheduleRepo.Update(item);

			}
			else
			{
				throw new NullReferenceException(ExceptionMessages.ClassDanceNotFound);
			}

			await this.scheduleRepo.SaveChangesAsync();
			return item;


		}

		public async Task<IEnumerable<Class>> GetClasses()
		{
			return await this.classRepository.All().ToListAsync();
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

namespace JustDanceAcademy.Services.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Web.ViewModels.Models;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;

	public class ScheduleServiceTests
	{
		private Mock<IRepository<Schedule>> scheduleRepo;
		private Mock<IRepository<Class>> repo;

		public ScheduleServiceTests()
		{
			this.scheduleRepo = new Mock<IRepository<Schedule>>();
			this.repo = new Mock<IRepository<Class>>();
		}

		[Fact]
		public async Task AddScheduleShouldReturnValidId()
		{
			var schedule = new AddScheduleViewModel
			{
				Id = 1,
				Age = JustDanceAcademy.Data.Models.Enum.Age.Adult,
				AllClasses = new List<Class>(),
				Day = JustDanceAcademy.Data.Models.Enum.Day.Tuesday,
				ClassId = 3,
				LevelCategory = "InterMediate",
				StartClass = DateTime.Now,
				EndClass = DateTime.UtcNow,
			};
			var list = new List<Schedule>();

			this.scheduleRepo.Setup(x => x.AddAsync(It.IsAny<Schedule>()))
						.Callback(() =>
						{
							return;
						});
			this.scheduleRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.scheduleRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());


			var service = new ScheduleService(this.scheduleRepo.Object, null, null);
			var result = await service.CreateSchedule(schedule);


			this.scheduleRepo.Verify(
				x => x.AddAsync(It.IsAny<Schedule>()),
				Times.Once());
			this.scheduleRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

			Assert.Equal(result, schedule.Id);
		}

		[Fact]
		public async Task DeleteColumnFromScheduleTableShouldWorkCorrectly()
		{
			var schedule = new Schedule
			{
				Id = 10,
				LevelCategory = "Begginer",
			};

			var list = new List<Schedule>();
			list.Add(schedule);

			this.scheduleRepo.Setup(m => m.Update(It.IsAny<Schedule>()))
				.Callback(() => { list.Remove(schedule); });
			this.scheduleRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.scheduleRepo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			var service = new ScheduleService(this.scheduleRepo.Object, null, null);

			var result = await service.DeleteColumn(schedule.Id);

			Assert.True(result.IsDeleted);

		}

		[Fact]
		public async Task GetScheduleByIdShouldReturnValidId()
		{
			var schedule = new Schedule
			{
				Id = 10,
				LevelCategory = "Begginer",
			};

			var list = new List<Schedule>();
			list.Add(schedule);

			this.scheduleRepo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			var service = new ScheduleService(this.scheduleRepo.Object, null, null);


			var result = await service.GetScheduleById(schedule.Id);

			Assert.Equal(schedule.Id, result);

		}

	}
}

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
		public async Task GetAllSchedulesShouldReturnValidView()
		{
			var classes = InstructorServiceTests.GetClassesList();

			var list = new List<Schedule>();

			var scheduleOne = new Schedule
			{
				Id = 1,
				Class = classes[0],
				ClassId = 1,
				Day = JustDanceAcademy.Data.Models.Enum.Day.Monday,
			};


			var scheduleTwo = new Schedule
			{
				Id = 2,
				Class = classes[1],
				ClassId = 2,
				Day = JustDanceAcademy.Data.Models.Enum.Day.Tuesday,
			};


			var scheduleThree = new Schedule
			{
				Id = 3,
				Class = classes[2],
				ClassId = 3,
				Day = JustDanceAcademy.Data.Models.Enum.Day.Friday,
			};
			list.Add(scheduleOne);
			list.Add(scheduleTwo);
			list.Add(scheduleThree);

			this.scheduleRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable().BuildMock());

			var service = new ScheduleService(this.scheduleRepo.Object, null, null);

			var result = await service.AllSchedules();

			Assert.Equal(list.Count(), result.Count());

		}

		[Fact]
		public async Task AddScheduleShouldReturnValidId()
		{
			var categories = LevelCategoryServiceTests.GetLevelDancingList();

			var schedule = new AddScheduleViewModel
			{
				Id = 1,
				Age = JustDanceAcademy.Data.Models.Enum.Age.Adult,
				AllClasses = new List<Class>(),
				Day = JustDanceAcademy.Data.Models.Enum.Day.Tuesday,
				ClassId = 3,
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
				ClassId = 1,
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
				ClassId = 3,
			};

			var list = new List<Schedule>();
			list.Add(schedule);

			this.scheduleRepo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			var service = new ScheduleService(this.scheduleRepo.Object, null, null);


			var result = await service.GetScheduleById(schedule.Id);

			Assert.Equal(schedule.Id, result);

		}

		[Fact]

		public async Task GetClassesShouldReturnAllPlusTheirCategories()
		{
			var classesList = InstructorServiceTests.GetClassesList();

			this.repo.Setup(x => x.All()).Returns(classesList.AsQueryable().BuildMock());

			var service = new ScheduleService(this.scheduleRepo.Object, this.repo.Object, null);
			var result = await service.GetClasses();

			Assert.Equal(classesList.Count(), result.Count());

			Assert.Equal(classesList[0].Name, result.Where(x => x.Id == classesList[0].Id).Select(x => x.Name).FirstOrDefault());

			Assert.Equal(classesList[2].Name, result.Where(x => x.Id == classesList[2].Id).Select(x => x.Name).FirstOrDefault());
		}

	}
}

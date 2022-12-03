namespace JustDanceAcademy.Services.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Models;
	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Web.ViewModels.Models;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;

	public class ClassServiceTests
	{
		private Mock<IRepository<Class>> repo;
		private Mock<IRepository<Review>> reviewRepo;
		private Mock<IRepository<ClassStudent>> comboRepo;
		private Mock<IRepository<ApplicationUser>> userRepo;
		private Mock<IRepository<MemberShip>> planRepo;
		private Mock<IRepository<Schedule>> scheduleRepo;

		public ClassServiceTests()
		{
			this.repo = new Mock<IRepository<Class>>();
			this.reviewRepo = new Mock<IRepository<Review>>();
			this.comboRepo = new Mock<IRepository<ClassStudent>>();
			this.userRepo = new Mock<IRepository<ApplicationUser>>();
			this.planRepo = new Mock<IRepository<MemberShip>>();
			this.scheduleRepo = new Mock<IRepository<Schedule>>();
		}

		[Fact]
		public async Task GetCountShouldReturnCorrectNumber()
		{
			var classes = new List<Class>
			{
				new Class(),
				new Class(),
				new Class(),

			}.AsQueryable();
			this.repo.Setup(r => r.AllAsNoTracking()).Returns(classes.BuildMock());

			var service = new ClassService(null, this.repo.Object, null, null, null, null, null);

			Assert.Equal(classes.Count(), await service.GetCountAsync());

			this.repo.Verify(x => x.AllAsNoTracking(), Times.Once);
		}

		[Fact]
		public async Task AddClassShouldReturnsValidIdOfAddedMethod()
		{
			var dance = this.CreateModel();

			var list = new List<Class>();

			this.repo.Setup(x => x.AddAsync(It.IsAny<Class>()))
				.Callback(() =>
				{
					return;
				});
			this.repo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.repo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new ClassService(null, this.repo.Object, null, null, null, null, null);
			var result = await service.CreateClassAsync(dance);

			this.repo.Verify(
				x => x.AddAsync(It.IsAny<Class>()),
				Times.Once());
			this.repo.Verify(x => x.SaveChangesAsync(), Times.Once());

			Assert.Equal(result, dance.Id);

		}

		[Fact]
		public async Task AddClassShouldIncreaseDancecWithOne()
		{
			var dance = this.CreateModel();
			var list = new List<Class>();

			this.repo.Setup(x => x.AddAsync(It.IsAny<Class>()))
			.Callback(() =>
			{
				return;
			});
			this.repo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.repo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new ClassService(null, this.repo.Object, null, null, null, null, null);

			var result = await service.GetCountAsync();
			Assert.Equal(list.Count(), result);

			await service.CreateClassAsync(dance);
			var finalResult = await service.GetCountAsync();

			Assert.Equal(list.Count(), finalResult);
		}

		[Fact]
		public async Task DeleteClassById()
		{
			// Delete Class  With Added Review/Students' properties like phones'and delete's classId
			var dance = new Class
			{
				Id = 5,
				Students = new List<ClassStudent>(),
			};


			var list = new List<Class>();

			var userClass = new ApplicationUser
			{
				Id = "2",
				PhoneNumber = "taken",
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(userClass);

			list.Add(dance);
			this.repo.Setup(m => m.Update(It.IsAny<Class>()))
				.Callback(() => { list.Remove(dance); });
			this.repo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			var review = new Review { ClassId = 5 };
			var reviewList = new List<Review>
			{
				review,
			};
			this.reviewRepo.Setup(m => m.Update(It.IsAny<Review>()))
							.Callback(() => { reviewList.Remove(review); });
			this.reviewRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.reviewRepo.Setup(x => x.All()).Returns(reviewList.AsQueryable().BuildMock());

			var student = new ClassStudent { ClassId = 5, StudentId = "2", Class = dance };
			dance.Students.Add(student);
			this.comboRepo.Setup(m => m.Update(It.IsAny<ClassStudent>()))
							.Callback(() => { dance.Students.Remove(student); });
			this.comboRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.comboRepo.Setup(x => x.All()).Returns(dance.Students.AsQueryable().BuildMock());

			this.userRepo.Setup(m => m.Update(It.IsAny<ApplicationUser>()))
							.Callback(() => { userList.Remove(userClass); });
			this.userRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var schedule = new Schedule()
			{
				ClassId = 5,
			};
			var listedSchedule = new List<Schedule>
			{
				schedule,
			};
			this.scheduleRepo.Setup(m => m.Update(It.IsAny<Schedule>()))
							.Callback(() => { listedSchedule.Remove(schedule); });
			this.scheduleRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.scheduleRepo.Setup(x => x.All()).Returns(listedSchedule.AsQueryable().BuildMock());

			var service = new ClassService(
				this.comboRepo.Object,
				this.repo.Object,
				this.userRepo.Object,
				null,
				null,
				this.reviewRepo.Object,
				this.scheduleRepo.Object);

			var result = await service.DeleteClass(dance.Id);

			Assert.True(result.IsDeleted);
		}

		[Fact]
		public async Task DeleteClassByIdShouldThrowException()
		{
			var dance = new Class
			{
				Id = 5,
				Students = new List<ClassStudent>()
			};
			var list = new List<Class>();
			list.Add(dance);

			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			var service = new ClassService(null, this.repo.Object, null, null, null, null, null);

			var ex = await Assert.ThrowsAsync<NullReferenceException>
				(
				async () => await service.DeleteClass(8)
				);

			await Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteClass(77));

			Assert.Equal(ExceptionMessages.ClassDanceNotFound, ex.Message);



		}


		[Fact]
		public async Task AddPlanShouldReturnsValidPlanViewModel()
		{
			var plan = this.CreatePlan();

			var list = new List<MemberShip>();

			this.planRepo.Setup(x => x.AddAsync(It.IsAny<MemberShip>()))
				.Callback(() =>
				{
					return;
				});
			this.planRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.planRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new ClassService(null, null, null, this.planRepo.Object, null, null, null);
			var result = await service.CreatePlan(plan);

			this.planRepo.Verify(
				x => x.AddAsync(It.IsAny<MemberShip>()),
				Times.Once());
			this.planRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

			Assert.Equal(result, plan.Id);

		}

		[Fact]
		public async Task GetAllShouldReturnAllClasses()
		{
			var dances = GetCollectionOfClasses();
			this.repo.Setup(x => x.AllAsNoTracking()).Returns(dances.ToList().BuildMock());

			var service = new ClassService(null, this.repo.Object, null, null, null, null, null);

			var result = await service.GetAllAsync();

			Assert.Equal(dances.Count(), result.Count());

			this.repo.Verify(x => x.AllAsNoTracking(), Times.Once);

			Assert.Equal(dances[0].Name, result.Where(x => x.Id == dances[0].Id).Select(x => x.Name).FirstOrDefault());

			Assert.Equal(dances[1].Instructor, result.Where(x => x.Id == dances[1].Id).Select(x => x.Instructor).FirstOrDefault());
		}

		private AddClassViewModel CreateModel()
		{
			return new AddClassViewModel
			{
				Id = 6,
				Name = "Test",
				Instructor = "Test First",
				ImageUrl = "https://thumbs.dreamstime.com/z/dance-logo-design-symbol-dance-logo-design-symbol-art-125584033.jpg",
				LevelCategoryId = 2,
				Description = " The following description is for test",


			};
		}



		private PlanViewModel CreatePlan()
		{
			return new PlanViewModel
			{
				Title = "Test",
				Price = 470,
				Age = JustDanceAcademy.Data.Models.Enum.Age.Teen,
				AgeRequirement = "Must be between 16-18 ",



			};
		}
	}
}

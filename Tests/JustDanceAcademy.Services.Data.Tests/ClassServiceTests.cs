namespace JustDanceAcademy.Services.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.Contracts;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Models;
	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Web.ViewModels.Administration.Users;
	using JustDanceAcademy.Web.ViewModels.Models;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;
	using static NUnit.Framework.Constraints.Tolerance;

	public class ClassServiceTests
	{
		private Mock<IRepository<Class>> repo;
		private Mock<IRepository<Review>> reviewRepo;
		private Mock<IRepository<ClassStudent>> comboRepo;
		private Mock<IRepository<ApplicationUser>> userRepo;
		private Mock<IRepository<MemberShip>> planRepo;
		private Mock<IRepository<Schedule>> scheduleRepo;
		private Mock<IRepository<LevelCategory>> levelRepo;


		public ClassServiceTests()
		{
			this.repo = new Mock<IRepository<Class>>();
			this.reviewRepo = new Mock<IRepository<Review>>();
			this.comboRepo = new Mock<IRepository<ClassStudent>>();
			this.userRepo = new Mock<IRepository<ApplicationUser>>();
			this.planRepo = new Mock<IRepository<MemberShip>>();
			this.scheduleRepo = new Mock<IRepository<Schedule>>();
			this.levelRepo = new Mock<IRepository<LevelCategory>>();


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
		public async Task AddReviewToAClassReturnsValidReviewModel()
		{
			var dance = new Class
			{
				Id = 5,
				Name = "tiktologia",
				Students = new List<ClassStudent>(),
			};

			var list = new List<Class>();

			var userClass = new ApplicationUser
			{
				Id = "2",
				UserName = "badGirlivsy",
				Class = dance,
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(userClass);

			list.Add(dance);

			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			var student = new ClassStudent { ClassId = 5, StudentId = "2", Class = dance };
			dance.Students.Add(student);

			this.comboRepo.Setup(x => x.All()).Returns(dance.Students.AsQueryable().BuildMock());

			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var review = new ReviewViewModel
			{
				Id = 1,
				Student = userClass.UserName,
				ClassId = dance.Id,
				Context = "I hope this test be passed success",
				NameClass = dance.Name,
			};

			var reviewList = new List<Review>();
			this.reviewRepo.Setup(x => x.AddAsync(It.IsAny<Review>()))
					.Callback(() =>
					{
						return;
					});
			this.reviewRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.reviewRepo.Setup(x => x.AllAsNoTracking()).
				Returns(reviewList.AsQueryable().BuildMock());

			var service = new ClassService(this.comboRepo.Object, this.repo.Object, this.userRepo.Object, null, null, this.reviewRepo.Object, null);

			var result = await service.CreateReview(dance.Id, userClass.Id, review);

			this.reviewRepo.Verify(
				x => x.AddAsync(It.IsAny<Review>()),
				Times.Once());
			this.repo.Verify(x => x.SaveChangesAsync(), Times.Once());

			Assert.Equal(result, review.Id);
		}

		[Fact]
		public async Task AddReviewToANoExistingClassShouldThrowError()
		{

			var dance = new Class
			{
				Id = 5,
				Name = "tiktologia",
				Students = new List<ClassStudent>(),
			};


			var list = new List<Class>();

			var userClass = new ApplicationUser
			{
				Id = "2",
				UserName = "badGirlivsy",
				Class = dance,
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(userClass);

			list.Add(dance);

			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());


			var review = new ReviewViewModel
			{
				Id = 1,
				Student = userClass.UserName,
				ClassId = dance.Id,
				Context = "I hope this test be passed success",
				NameClass = dance.Name,
			};

			var reviewList = new List<Review>();
			this.reviewRepo.Setup(x => x.AddAsync(It.IsAny<Review>()))
					.Callback(() =>
					{
						return;
					});
			this.reviewRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.reviewRepo.Setup(x => x.AllAsNoTracking()).
				Returns(reviewList.AsQueryable().BuildMock());

			var service = new ClassService(null, this.repo.Object, null, null, null, this.reviewRepo.Object, null);

			var ex = await Assert.ThrowsAsync<NullReferenceException>(
			async () => await service.CreateReview(8, userClass.Id, review));

			await Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateReview(77, userClass.Id, review));

			Assert.Equal(ExceptionMessages.ClassDanceNotFound, ex.Message);

		}

		[Fact]
		public async Task AddingReviewFromAnEmptyUserIdShouldThrowError()
		{

			var dance = new Class
			{
				Id = 5,
				Name = "tiktologia",
				Students = new List<ClassStudent>(),
			};


			var list = new List<Class>();

			var userClass = new ApplicationUser
			{
				Id = "2",
				UserName = "badGirlivsy",
				Class = dance,
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(userClass);

			list.Add(dance);

			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());


			var review = new ReviewViewModel
			{
				Id = 1,
				Student = userClass.UserName,
				ClassId = dance.Id,
				Context = "I hope this test be passed success",
				NameClass = dance.Name,
			};

			var reviewList = new List<Review>();
			this.reviewRepo.Setup(x => x.AddAsync(It.IsAny<Review>()))
					.Callback(() =>
					{
						return;
					});
			this.reviewRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.reviewRepo.Setup(x => x.AllAsNoTracking()).
				Returns(reviewList.AsQueryable().BuildMock());

			var service = new ClassService(null, this.repo.Object, this.userRepo.Object, null, null, this.reviewRepo.Object, null);

			var ex = await Assert.ThrowsAsync<NullReferenceException>(
			async () => await service.CreateReview(dance.Id, "150", review));

			await Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateReview(dance.Id, "120", review));

			Assert.Equal(ExceptionMessages.ClassDanceNotFound, ex.Message);
		}

		[Fact]
		public async Task AddReviewToANotStartedClassShouldThrowError()
		{

			var dance = new Class
			{
				Id = 5,
				Name = "tiktologia",
				Students = new List<ClassStudent>(),
			};


			var testDance = new Class
			{
				Id = 8,
				Name = "SoftDeleteVS",
				Students = new List<ClassStudent>(),
			};


			var list = new List<Class>();

			var userClass = new ApplicationUser
			{
				Id = "2",
				UserName = "badGirlivsy",
				Class = dance,
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(userClass);

			list.Add(dance);
			list.Add(testDance);

			var student = new ClassStudent { ClassId = 5, StudentId = "2", Class = dance };
			dance.Students.Add(student);

			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());


			this.comboRepo.Setup(x => x.All()).Returns(dance.Students.AsQueryable().BuildMock());


			var review = new ReviewViewModel
			{
				Id = 1,
				Student = userClass.UserName,
				ClassId = dance.Id,
				Context = "I hope this test be passed success",
				NameClass = dance.Name,
			};

			var reviewList = new List<Review>();
			this.reviewRepo.Setup(x => x.AddAsync(It.IsAny<Review>()))
					.Callback(() =>
					{
						return;
					});
			this.reviewRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.reviewRepo.Setup(x => x.AllAsNoTracking()).
				Returns(reviewList.AsQueryable().BuildMock());

			var service = new ClassService(this.comboRepo.Object, this.repo.Object, this.userRepo.Object, null, null, this.reviewRepo.Object, null);

			var ex = await Assert.ThrowsAsync<ArgumentException>(
			async () => await service.CreateReview(testDance.Id, userClass.Id, review));

			await Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateReview(testDance.Id, userClass.Id, review));

			Assert.Equal(ExceptionMessages.ReviewNotAllowed, ex.Message);

		}

		[Fact]
		public async Task StartingAClassWorkCorrectly()
		{

			var dance = new Class
			{
				Id = 5,
				Name = "tiktologia",
				Students = new List<ClassStudent>(),
			};


			var list = new List<Class>();

			var user = new ApplicationUser
			{
				Id = "2",
				UserName = "badGirlivsy",
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);

			list.Add(dance);

			this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());



			var userClass = new ClassStudent
			{
				Student = user,
				StudentId = user.Id,
				Class = dance,
				ClassId = dance.Id,
			};
			var allListOfUserClass = new List<ClassStudent>();

			this.comboRepo.Setup(x => x.AddAsync(It.IsAny<ClassStudent>()))
					.Callback(() =>
					{
						return;
					});
			this.comboRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.comboRepo.Setup(x => x.AllAsNoTracking()).
				Returns(allListOfUserClass.AsQueryable().BuildMock());

			var service = new ClassService(this.comboRepo.Object, this.repo.Object, this.userRepo.Object, null, null, null, null);

			await service.AddStudentToClass(user.Id, dance.Id);

			this.comboRepo.Verify(m => m.AddAsync(It.IsAny<ClassStudent>()), Times.Once());
			this.comboRepo.Verify(m => m.SaveChangesAsync(), Times.Once());
		}

		[Fact]
		// ADD OPITON : IF USER PAID CLASS CANNOT LEAVE THE CLASS-CLASS SERVICE TODO;
		public async Task LeavingAClassShouldWorkProperly()
		{
			var dance = new Class
			{
				Id = 5,
				Students = new List<ClassStudent>(),
			};


			var list = new List<Class>();

			var user = new ApplicationUser
			{
				Id = "2",
				PhoneNumber = "taken",
				Class = dance,
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);

			list.Add(dance);

			var student = new ClassStudent { ClassId = 5, StudentId = "2", Class = dance };
			dance.Students.Add(student);

			this.userRepo.Setup(m => m.Update(It.IsAny<ApplicationUser>()))
							.Callback(() => { userList.Remove(user); });
			this.userRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			this.comboRepo.Setup(m => m.Update(It.IsAny<ClassStudent>()))
							.Callback(() => { dance.Students.Remove(student); });
			this.comboRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.comboRepo.Setup(x => x.All()).Returns(dance.Students.AsQueryable().BuildMock());

			var service = new ClassService(
				this.comboRepo.Object,
				null,
				this.userRepo.Object,
				null,
				null,
				null,
				null);

			var result = await service.LeaveClass(user.Id);

			Assert.True(result.IsDeleted);

		}

		[Fact]
		public async Task TakePhoneNumberIsOrderToPayWorkCorrectly()
		{
			var user = new ApplicationUser
			{
				Id = "2",
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);
			this.userRepo.Setup(m => m.Update(It.IsAny<ApplicationUser>()))
							.Callback(() => { return; });
			this.userRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var service = new ClassService(null, null, this.userRepo.Object, null, null, null, null);
			var result = await service.TakeNumberForStart(user.Id);

			Assert.Equal("taken", result.PhoneNumber);
		}

		[Fact]
		public async Task UserPhoneNumberMustBeUnTakenInOrderToPay()
		{
			var user = new ApplicationUser
			{
				Id = "2",
				ClassId = 5,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);
			this.userRepo.Setup(m => m.Update(It.IsAny<ApplicationUser>()))
							.Callback(() => { return; });
			this.userRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var service = new ClassService(null, null, this.userRepo.Object, null, null, null, null);
			var result = await service.PhoneNotifyForClass(user.Id);

			Assert.False(result);

		}

		[Fact]
		public async Task UserCannotPayTwiceWithTakenPhoneNumberCondition()
		{
			var user = new ApplicationUser
			{
				Id = "2",
				ClassId = 5,
				PhoneNumber = "taken",
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);
			this.userRepo.Setup(m => m.Update(It.IsAny<ApplicationUser>()))
							.Callback(() => { return; });
			this.userRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var service = new ClassService(null, null, this.userRepo.Object, null, null, null, null);
			var result = await service.PhoneNotifyForClass(user.Id);

			Assert.True(result);

		}

		[Fact]
		public async Task FindClassNameForReviewShouldWorkProperly()
		{
			var dance = new Class { Id = 5, Name = "YouRelyOn", Students = new List<ClassStudent>() };

			var danceList = new List<Class>();
			danceList.Add(dance);

			var user = new ApplicationUser { Id = "1", ClassId = dance.Id, Class = dance };

			var userClass = new ClassStudent
			{
				Student = user,
				StudentId = user.Id,
				ClassId = dance.Id,
				Class = dance,
			};

			var userList = new List<ApplicationUser>();
			userList.Add(user);
			dance.Students.Add(userClass);

			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());
			this.repo.Setup(x => x.All()).Returns(danceList.AsQueryable().BuildMock());
			this.comboRepo.Setup(x => x.All()).Returns(dance.Students.AsQueryable().BuildMock());


			var service = new ClassService(this.comboRepo.Object, this.repo.Object, this.userRepo.Object, null, null, null, null);
			var result = await service.GetClassForReview(user.Id);

			Assert.Equal(dance.Name, result);


		}

		[Fact]
		public async Task UserWithNoClassIdShouldReturnFalsy()
		{
			var user = new ApplicationUser
			{
				Id = "2",
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var service = new ClassService(null, null, this.userRepo.Object, null, null, null, null);
			var result = await service.DoesUserHaveClass(user.Id);

			Assert.False(result);
		}

		[Fact]
		public async Task DoesUserHaveClassWithClassIdShouldBeTrue()
		{

			var user = new ApplicationUser
			{
				Id = "2",
				ClassId = 10,
			};
			var userList = new List<ApplicationUser>();
			userList.Add(user);
			this.userRepo.Setup(x => x.All()).Returns(userList.AsQueryable().BuildMock());

			var service = new ClassService(null, null, this.userRepo.Object, null, null, null, null);
			var result = await service.DoesUserHaveClass(user.Id);

			Assert.True(result);
		}

		[Fact]
		public async Task EditClassShouldUpdatePropertiesCorrectly()
		{
			var categories = LevelCategoryServiceTests.GetLevelDancingList();

			var dance = new Class { Id = 1, Name = "YouRelyOn", LevelCategory = categories[0], LevelCategoryId = 1, Description = "Lets start again", Instructor = "Zendayaa" };

			var danceList = new List<Class>();
			danceList.Add(dance);

			var chosenClass = new EditDanceViewModel { Id = 1, Description = "Test Must beUpdate", Instructor = "Bella Torres" };

			this.repo.Setup(x => x.All()).Returns(danceList.AsQueryable().BuildMock());
			this.levelRepo.Setup(x => x.All()).Returns(categories.AsQueryable().BuildMock());



			var service = new ClassService
			(
				null,
				this.repo.Object,
				null,
				null,
				this.levelRepo.Object,
				null,
				null
			);
			await service.Edit(dance.Id, chosenClass);

			var updatedClass = danceList.FirstOrDefault(x => x.Id == 1);

			Assert.Equal(chosenClass.Description, updatedClass.Description);
			Assert.Equal(chosenClass.Instructor, updatedClass.Instructor);
			this.repo.Verify(x => x.SaveChangesAsync(), Times.Once());
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
		//private static List<Class> GetClasses()
		//{
		//	var categories = LevelCategoryServiceTests.GetLevelDancingList();
		//	return new List<Class>()
		//	{
		//		new Class
		//		{
		//			Id = 6,
		//			Name = "Test",
		//			LevelCategory = categories[0],
		//			LevelCategoryId=1,
		//			Instructor = "Gabriel Otega",
		//			Description = "This is final Call Please",
		//			ImageUrl = "https://www.graphicsprings.com/filestorage/stencils/ab7e8c6f9711a06c5fb4abb593715a3e.png?width=500&height=500",
		//		},
		//		new Class
		//		{
		//			Id = 7,
		//			Name = "Test",
		//			LevelCategory = categories[1],
		//			LevelCategoryId = 2,
		//			Instructor = "Bella Hidiit",
		//			Description = "This is final Call Please",
		//			ImageUrl = "https://www.graphicsprings.com/filestorage/stencils/ab7e8c6f9711a06c5fb4abb593715a3e.png?width=500&height=500",
		//		},
		//		new Class
		//		{
		//			Id = 8,
		//			Name = "Test",
		//			LevelCategory = categories[2],
		//			LevelCategoryId = 3,
		//			Instructor = "Jenna Orgeta",
		//			Description = "This is final Call Please",
		//			ImageUrl = "https://www.graphicsprings.com/filestorage/stencils/ab7e8c6f9711a06c5fb4abb593715a3e.png?width=500&height=500",
		//		},
		//	};
		//}
	}
}

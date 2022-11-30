namespace JustDanceAcademy.Services.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Data.Repositories;
	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Services.Data.Tests;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Cors.Infrastructure;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;


	public class ClassServiceTests
	{
		private Mock<IRepository<Class>> repo;
		private Mock<IRepository<Review>> reviewRepo;
		private Mock<IRepository<ClassStudent>> comboRepo;
		private Mock<IRepository<ApplicationUser>> userRepo;


		public ClassServiceTests()
		{
			this.repo = new Mock<IRepository<Class>>();
			this.reviewRepo = new Mock<IRepository<Review>>();
			this.comboRepo = new Mock<IRepository<ClassStudent>>();
			this.userRepo = new Mock<IRepository<ApplicationUser>>();

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

			var service = new ClassService(null, this.repo.Object, null, null, null, null);

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

			var service = new ClassService(null, this.repo.Object, null, null, null, null);
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

			var service = new ClassService(null, this.repo.Object, null, null, null, null);

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
				Students = new List<ClassStudent>()
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
			var reviewList = new List<Review>();
			reviewList.Add(review);
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

			var service = new ClassService(this.comboRepo.Object, this.repo.Object, this.userRepo.Object, null, null, this.reviewRepo.Object);

			var result = await service.DeleteClass(dance.Id);

			Assert.True(result.IsDeleted);



			//var dance = new Class { Id = 5};

			//var list = new List<Class>();
			//list.Add(dance);

			//var count = list.Count();

			//this.repo.Setup(m => m.Update(It.IsAny<Class>()))
			//	.Callback(() => { list.Remove(dance); });
			//this.repo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });
			//this.repo.Setup(x => x.All()).Returns(list.AsQueryable().BuildMock());

			//this.reviewRepo.Setup(m => m.Update(It.IsAny<Review>()))
			//	.Callback(() => { list.Remove(dance); });
			//this.reviewRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });

			//this.userRepo.Setup(m => m.Update(It.IsAny<ApplicationUser>()))
			//	.Callback(() => { list.Remove(dance); });
			//this.userRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });

			//this.comboRepo.Setup(m => m.Update(It.IsAny<ClassStudent>()))
			//	.Callback(() => { list.Remove(dance); });
			//this.comboRepo.Setup(x => x.SaveChangesAsync()).Callback(() => { return; });


			//var service = new ClassService(null, this.repo.Object, null, null, null, null);

			//var danceClass = await service.CreateClassAsync(this.CreateModel());
			//var result = await service.DeleteClass(danceClass);

			//this.repo.Verify(x=>x.Update(It.IsAny<Class>()),Times.Once);
			//this.repo.Verify(x => x.SaveChangesAsync(), Times.Once());


			Assert.True(result.IsDeleted);

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
	}
}
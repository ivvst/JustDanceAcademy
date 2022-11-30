namespace JustDanceAcademy.Services.Data.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Data.Repositories;
	using JustDanceAcademy.Services.Data.Tests;
	using JustDanceAcademy.Web.ViewModels.Models;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;

	public class ClassServiceTests
	{
		private Mock<IRepository<Class>> repo;

		public ClassServiceTests()
		{
			this.repo = new Mock<IRepository<Class>>();
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
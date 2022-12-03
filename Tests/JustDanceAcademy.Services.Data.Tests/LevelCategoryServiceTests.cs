namespace JustDanceAcademy.Services.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Data.Repositories;
	using JustDanceAcademy.Web.ViewModels.Models;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;

	public class LevelCategoryServiceTests
	{
		private Mock<IRepository<LevelCategory>> repo;

		public LevelCategoryServiceTests()
		{
			this.repo = new Mock<IRepository<LevelCategory>>();
		}

		[Fact]
		public async Task GetAllShouldReturnAllListWithLevels()
		{
			var levelsList = GetLevelDancingList();
			this.repo.Setup(x => x.AllAsNoTracking())
				.Returns(levelsList.AsQueryable().BuildMock());

			var service = new LevelDanceService(this.repo.Object);
			var result = await service
				.AllCategories();

			Assert.Equal(levelsList.Count(), result.Count());

			Assert.Equal(levelsList[0].Name, result.Where(x =>
			x.Id == levelsList[0].Id)
				.Select(x => x.Name).FirstOrDefault());

			Assert.Equal(levelsList[2].Name, result.Where(x =>
			x.Id == levelsList[2].Id)
				.Select(x => x.Name).FirstOrDefault());
		}



		public static List<LevelCategory> GetLevelDancingList()
		{
			return new List<LevelCategory>
			{
				new LevelCategory { Id = 1, Name = "Getting Started" },
				new LevelCategory { Id = 2, Name = "Begginer"},
				new LevelCategory { Id = 3, Name = "InterMediate" },
				new LevelCategory { Id = 4, Name = "Advanced" },
				new LevelCategory { Id = 5, Name = "Kids" },
			};
		}
	}
}

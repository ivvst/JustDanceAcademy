namespace JustDanceAcademy.Services.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Web.ViewModels.Models;
	using MockQueryable.Moq;
	using Moq;
	using Xunit;

	public class InstructorServiceTests
	{

		private Mock<IRepository<Instrustor>> trainerRepo;

		private Mock<IRepository<Class>> classRepo;

		public InstructorServiceTests()
		{
			this.classRepo = new Mock<IRepository<Class>>();
			this.trainerRepo = new Mock<IRepository<Instrustor>>();

		}

		[Fact]
		public async Task GetAllInstructorsShouldReturnValidView()
		{
			var classes = GetClassesList();
			//var categories = LevelCategoryServiceTests.GetLevelDancingList();
			var list = new List<Instrustor>();

			var trainerOne = new Instrustor
			{
				Id = 1,
				Name = "TestOfOne",
				Class = classes[0],
				ClassId = 1,
			};
			var trainerTwo = new Instrustor
			{
				Id = 1,
				Name = "TestOfTwo",
				Class = classes[1],
				ClassId = 2,
			};
			list.Add(trainerOne);
			list.Add(trainerTwo);

			this.trainerRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, null);
			var result = await service.GetAllInstructors();

			Assert.Equal(list.Count(), result.Count());

		}

		[Fact]
		public async Task TrainerWithExistingNameShouldThrowsError()
		{
			var trainer = new Instrustor
			{
				Id = 1,
				Name = "Pamela Reif",
			};

			var trainerTwo = new Instrustor
			{

				Id = 2,
				Name = "Pamela Reif",
			};

			var list = new List<Instrustor>();
			list.Add(trainer);

			this.trainerRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, null);
			var result = await service.DoesInstructorExist(trainerTwo.Name);

			Assert.True(result);


		}

		[Fact]
		public async Task TrainerWithNoExistingNameShouldBeAddedTo()
		{
			var trainer = new Instrustor
			{
				Id = 1,
				Name = "Pamela Reif",
			};

			var trainerTwo = new Instrustor
			{

				Id = 2,
				Name = "Pamela Anderson",
			};

			var list = new List<Instrustor>();
			list.Add(trainer);

			this.trainerRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, null);
			var result = await service.DoesInstructorExist(trainerTwo.Name);

			Assert.False(result);


		}


		[Fact]
		public async Task AddTrainerWithExistingNameShouldReturnValidId()
		{
			var trainer = new InstructorViewModel
			{
				Id = 1,
				FullName = "Pamela Reif",
				ClassId = 6,
				ImageUrl = "https://thumbs.dreamstime.com/z/dance-logo-design-symbol-dance-logo-design-symbol-art-125584033.jpg",
				ClassesOfInstructor = new List<Class>(),
				AboutYou = "The test should work correctly await...",

			};
			var list = new List<Instrustor>();

			this.trainerRepo.Setup(x => x.AddAsync(It.IsAny<Instrustor>()))
						.Callback(() =>
						{
							return;
						});
			this.trainerRepo.Setup(x => x.SaveChangesAsync()).Callback(() =>
			{
				return;
			});
			this.trainerRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, null);
			var result = await service.AddInstructor(trainer);


			this.trainerRepo.Verify(
				x => x.AddAsync(It.IsAny<Instrustor>()),
				Times.Once());
			this.trainerRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

			Assert.Equal(result, trainer.Id);


		}

		[Fact]
		public async Task EditTrainerShouldUpdatePropertiesCorrectly()
		{
			var classes = GetClassesList();

			var trainer = new Instrustor
			{
				Id = 1,
				Name = "Pamela Reif",
				Biography = "Born in early 90's she become one of the most popular dance-trainer during 2020.",
				Class = classes[0],
				ClassId = 1,
			};

			var trainersList = new List<Instrustor>();
			trainersList.Add(trainer);

			var chosenTrainer = new InstructorViewModel
			{
				Id = 1,
				AboutYou = "Born in 1990 she attract people with her pure beauty",
				FullName = "Nina Dobrev",
			};

			this.trainerRepo.Setup(x => x.All()).Returns(trainersList.AsQueryable().BuildMock());
			this.classRepo.Setup(x => x.All()).Returns(classes.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object,
				this.classRepo.Object);

			await service.Edit(trainer.Id, chosenTrainer);

			var updatedTrainer = trainersList.FirstOrDefault(x => x.Id == 1);

			Assert.Equal(chosenTrainer.FullName, updatedTrainer.Name);
			Assert.Equal(chosenTrainer.AboutYou, updatedTrainer.Biography);

			this.trainerRepo.Verify(x => x.SaveChangesAsync(), Times.Once());

		}

		[Fact]

		public async Task EditTrainerWithGivenNullClassShouldThrowsError()
		{
			var classes = GetClassesList();

			var trainer = new Instrustor
			{
				Id = 1,
				Name = "Pamela Reif",
				Biography = "Born in early 90's she become one of the most popular dance-trainer during 2020.",
				Class = classes[0],
				ClassId = 1,
			};

			var trainersList = new List<Instrustor>();
			trainersList.Add(trainer);

			var chosenTrainer = new InstructorViewModel
			{
				Id = 1,
				AboutYou = "Born in 1990 she attract people with her pure beauty",
				FullName = "Nina Dobrev",
				ClassId = 15,
			};
			this.trainerRepo.Setup(x => x.All()).Returns(trainersList.AsQueryable().BuildMock());
			this.classRepo.Setup(x => x.All()).Returns(classes.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, this.classRepo.Object);

			var ex = await Assert.ThrowsAsync<NullReferenceException>(
			async () => await service.Edit(trainer.Id, chosenTrainer));

			var updatedTrainer = trainersList.FirstOrDefault(x => x.Id == 1);

			await Assert.ThrowsAsync<NullReferenceException>(async () => await service.Edit(trainer.Id, chosenTrainer));

			Assert.Equal(ExceptionMessages.ClassDanceNotFound, ex.Message);

		}

		[Fact]
		public async Task EditTrainerWithNotExistTrainerShouldThrowsError()
		{
			var classes = GetClassesList();

			var trainer = new Instrustor
			{
				Id = 1,
				Name = "Pamela Reif",
				Biography = "Born in early 90's she become one of the most popular dance-trainer during 2020.",
				Class = classes[0],
				ClassId = 1,
			};
			var chosenTrainer = new InstructorViewModel
			{
				Id = 1,
				AboutYou = "Born in 1990 she attract people with her pure beauty",
				FullName = "Nina Dobrev",
				ClassId = 2,
			};

			var trainersList = new List<Instrustor>();
			trainersList.Add(trainer);

			this.trainerRepo.Setup(x => x.All()).Returns(trainersList.AsQueryable().BuildMock());
			this.classRepo.Setup(x => x.All()).Returns(classes.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, this.classRepo.Object);

			var ex = await Assert.ThrowsAsync<NullReferenceException>(
			async () => await service.Edit(150, chosenTrainer));


			await Assert.ThrowsAsync<NullReferenceException>(async () => await service.Edit(150, chosenTrainer));

			Assert.Equal(ExceptionMessages.InstructorNotFound, ex.Message);
		}

		[Fact]
		public async Task GetClassesShouldReturnAllListWithDances()
		{
			var classesList = GetClassesList();

			this.classRepo.Setup(x => x.All()).Returns(classesList.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, this.classRepo.Object);
			var result = await service.GetClasses();

			Assert.Equal(classesList.Count(), result.Count());

			Assert.Equal(classesList[0].Name, result.Where(x => x.Id == classesList[0].Id).Select(x => x.Name).FirstOrDefault());

			Assert.Equal(classesList[2].Name, result.Where(x => x.Id == classesList[2].Id).Select(x => x.Name).FirstOrDefault());
		}

		[Fact]
		public async Task FindClassIdByGivenTrainerShouldReturnClass()
		{
			var classes = GetClassesList();
			var trainer = new Instrustor
			{
				Id = 1,
				Name = "Pamela Reif",
				Class = classes[0],
				ClassId = 1,
			};
			var list = new List<Instrustor>();
			list.Add(trainer);


			this.trainerRepo.Setup(x => x.AllAsNoTracking()).
				Returns(list.AsQueryable().BuildMock());
			this.classRepo.Setup(x => x.All()).Returns(classes.AsQueryable().BuildMock());

			var service = new InstructorService(this.trainerRepo.Object, this.classRepo.Object);
			var result = await service.GetClassId(trainer.Id);

			Assert.Equal(trainer.ClassId, result);

		}



		public static List<Class> GetClassesList()
		{
			var categories = LevelCategoryServiceTests.GetLevelDancingList();

			return new List<Class>
			{
				new Class { Id = 1, Name = "Tear-Dance", Instructor = "Ignasio Montero", Description = "Middle Of the night", LevelCategory = categories[0], LevelCategoryId = 1 },
				new Class { Id = 2, Name = "Feel-Dance", Instructor = "Jason Derulo", Description = "Light down low", LevelCategory = categories[1], LevelCategoryId = 2 },
				new Class { Id = 3, Name = "Anger-Dance", Instructor = "Jame Ortega", Description = "Dance with my hands", LevelCategory = categories[2], LevelCategoryId = 3 },
			};
		}


	}
}

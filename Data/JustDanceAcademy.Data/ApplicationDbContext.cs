namespace JustDanceAcademy.Data
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Models;
	using JustDanceAcademy.Data.Configurations;
	using JustDanceAcademy.Data.Models;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;
	using static System.Net.WebRequestMethods;

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
			typeof(ApplicationDbContext).GetMethod(
				nameof(SetIsDeletedQueryFilter),
				BindingFlags.NonPublic | BindingFlags.Static);

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		public DbSet<CommonQuestion> FAQuestions
		{
			get; set;
		} = null!;

		public DbSet<Class> Classes
		{
			get; set;
		} = null!;

		public DbSet<ClassStudent> ClassStudents
		{
			get; set;
		}
		public DbSet<TestStudent> TestStudents
		{
			get; set;
		}

		public DbSet<Review> Reviews
		{
			get; set;
		}

		public DbSet<Instrustor> Instrustors
		{
			get; set;
		} = null!;



		public DbSet<MemberShip> Plans
		{
			get; set;
		} = null!;


		public DbSet<Setting> Settings
		{
			get; set;
		}

		public DbSet<Schedule> Schedules
		{
			get; set;
		}

		public DbSet<LevelCategory> LevelsCategory
		{
			get; set;
		} = null!;

		private ApplicationUser GuestUser
		{
			get; set;
		}

		private ApplicationUser AdminUser
		{
			get; set;
		}

		private LevelCategory StartLevel
		{
			get; set;
		}

		private LevelCategory BegginerLevel
		{
			get; set;
		}

		private LevelCategory InetmediateLevel
		{
			get; set;
		}

		private LevelCategory AdvancedLevel
		{
			get; set;
		}

		private LevelCategory KidsLevel
		{
			get; set;
		}

		private Class FirstClass
		{
			get; set;
		}

		private Instrustor Instrustor
		{

			get;
			set;
		}

		private CommonQuestion Question
		{
			get; set;
		}



		public override int SaveChanges() => this.SaveChanges(true);

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			this.ApplyAuditInfoRules();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
			this.SaveChangesAsync(true, cancellationToken);

		public override Task<int> SaveChangesAsync(
			bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = default)
		{
			this.ApplyAuditInfoRules();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{

			this.SeedQuestion();
			builder.Entity<CommonQuestion>()
				.HasData(this.Question);

			this.SeedUsers();
			builder.Entity<ApplicationUser>()
				.HasData(this.GuestUser, this.AdminUser);

			this.SeedInstructor();
			builder.Entity<Instrustor>()
				.HasData(this.Instrustor);

			this.SeedLevels();
			builder.Entity<LevelCategory>()
				.HasData(
				this.StartLevel,
				this.BegginerLevel,
				this.InetmediateLevel,
				this.AdvancedLevel,
				this.KidsLevel);

			this.SeedClasses();
			builder.Entity<Class>()
				.HasData(this.FirstClass);

			// Needed for Identity models configuration
			base.OnModelCreating(builder);

			this.ConfigureUserIdentityRelations(builder);

			EntityIndexesConfiguration.Configure(builder);

			var entityTypes = builder.Model.GetEntityTypes().ToList();

			// Set global query filter for not deleted entities only
			var deletableEntityTypes = entityTypes
				.Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
			foreach (var deletableEntityType in deletableEntityTypes)
			{
				var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
				method.Invoke(null, new object[] { builder });
			}

			// Disable cascade delete
			var foreignKeys = entityTypes
				.SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
			foreach (var foreignKey in foreignKeys)
			{
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}

		private void SeedQuestion()
		{
			this.Question = new CommonQuestion()
			{
				Id = 1,
				Question = "Can I start a class without payment?",
				Answear = "No, after you start to train a class you will receive details about payment if you don't pay during 7 days you will receive second email and then you will not have the opportunity to start a train. ",
			};
		}

		private void SeedClasses()
		{
			this.FirstClass = new Class()
			{
				Id = 1,
				Name = "Hip Hop",
				ImageUrl = "https://cf.ltkcdn.net/dance/images/std/224320-630x450-hip-hop.jpg",
				Description = "Keep your head up,with your feet on the ground",
				LevelCategoryId = 2,
				Instructor = "Aaliyah",
			};
		}

		private void SeedLevels()
		{
			this.StartLevel = new LevelCategory()
			{
				Id = 1,
				Name = "Getting Started",


			};
			this.BegginerLevel = new LevelCategory()
			{
				Id = 2,
				Name = "Begginer",
			};
			this.InetmediateLevel = new LevelCategory()
			{
				Id = 3,
				Name = "InterMediate",
			};
			this.AdvancedLevel = new LevelCategory()
			{
				Id = 4,
				Name = "Advanced",
			};

			this.KidsLevel = new LevelCategory()
			{
				Id = 5,
				Name = "Kids",
			};
		}



		private void SeedInstructor()
		{
			this.Instrustor = new Instrustor()
			{
				Id = 1,
				Name = "Aaliyah",
				Biography =
				"Aaliyah Dana Haughton was an American singer. She has been credited for helping to redefine contemporary R&B, pop and hip hop, earning her the nicknames the -Princess of R&B- and -Queen of Urban Pop-",
				ImageUrl = "https://th.bing.com/th/id/OIP.qe-SelqFhGlvOpoOrNKO-AHaJr?pid=ImgDet&rs=1",
				ClassId = 1,

			};
		}

		private void SeedUsers()
		{
			var hasher = new PasswordHasher<ApplicationUser>();

			this.GuestUser = new ApplicationUser()
			{
				Id = "200adb3d-b3f4-4bde-a9c8-2c6888d6be30",
				UserName = "guest",
				NormalizedUserName = "GUEST",
				Email = "guest@mail.com",
				NormalizedEmail = "GUEST@MAIL.com",

			};

			this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "guest");

			this.AdminUser = new ApplicationUser()
			{
				Id = "8fe346ea-30ce-4b6e-b67a-fedc225845c1",
				UserName = "vanis",
				NormalizedUserName = "VANIS",
				Email = "vanis@mail.com",
				NormalizedEmail = "VANIS@MAIL.com",

			};
			this.AdminUser.PasswordHash = hasher.HashPassword(this.AdminUser, "vanis");
		}

		private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
			where T : class, IDeletableEntity
		{
			builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
		}

		// Applies configurations
		private void ConfigureUserIdentityRelations(ModelBuilder builder)
			 => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

		private void ApplyAuditInfoRules()
		{
			var changedEntries = this.ChangeTracker
				.Entries()
				.Where(e =>
					e.Entity is IAuditInfo &&
					(e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach (var entry in changedEntries)
			{
				var entity = (IAuditInfo)entry.Entity;
				if (entry.State == EntityState.Added && entity.CreatedOn == default)
				{
					entity.CreatedOn = DateTime.UtcNow;
				}
				else
				{
					entity.ModifiedOn = DateTime.UtcNow;
				}
			}
		}
	}
}

namespace JustDanceAcademy.Web
{
	using System.Reflection;

	using JustDanceAcademy.Data;
	using JustDanceAcademy.Data.Common;
	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Data.Repositories;
	using JustDanceAcademy.Data.Seeding;
	using JustDanceAcademy.Services.Data;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Services.Mapping;
	using JustDanceAcademy.Services.Messaging;
	using JustDanceAcademy.Web.Hubs;
	using JustDanceAcademy.Web.ViewModels;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;


	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			ConfigureServices(builder.Services, builder.Configuration);
			var app = builder.Build();
			Configure(app);
			app.Run();
		}

		private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(
				options =>
				{
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

				});

			services.AddDefaultIdentity<ApplicationUser>(options =>
			{

				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;

			})
				.AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<CookiePolicyOptions>(
				options =>
				{
					options.CheckConsentNeeded = context => true;
					options.MinimumSameSitePolicy = SameSiteMode.None;
				});

			//services.AddDefaultIdentity<ApplicationUser>(options =>
			//{

			//    options.SignIn.RequireConfirmedAccount = false;
			//    options.Password.RequireDigit = false;
			//    options.Password.RequireNonAlphanumeric = false;
			//    options.Password.RequireLowercase = false;
			//    options.Password.RequireUppercase = false;

			//});
			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Administration/Account/Login";
				options.AccessDeniedPath = "/Administration/Account/AccessDenied";
				options.LogoutPath = "/Account/Logout";

			});

			services.AddRazorPages();
			services.AddSignalR();
			services.AddControllersWithViews(
				options =>
				{
					options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
				}).AddRazorRuntimeCompilation();
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddSingleton(configuration);

			// Data repositories
			services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
			services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddScoped<IDbQueryRunner, DbQueryRunner>();

			// Session
			services.AddDistributedMemoryCache();
			services.AddSession();

			// Application services
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IDanceClassService, ClassService>();
			services.AddScoped<ILevelCategoryService, LevelDanceService>();

			services.AddScoped<IServiceInstructor, InstructorService>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddTransient<IEmailSender, NullMessageSender>();
			services.AddTransient<ISettingsService, SettingsService>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		private static void Configure(WebApplication app)
		{
			//Seed data on application startup
			using (var serviceScope = app.Services.CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				dbContext.Database.Migrate();
				new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
			}

			AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseSession();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "areas",
				pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "danceDetails",
				pattern: "Class/ClassDetails/{id}/{information}");

			app.UseEndpoints(end =>
						{
							end.MapRazorPages();
							end.MapHub<NotificationHub>("/notificationHub");
						});
		}
	}
}

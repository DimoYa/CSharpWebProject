namespace MyResourcePlanning.Web
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyResourcePlanning.Data;
    using MyResourcePlanning.Data.Seeding;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Services.Data.Admin;
    using MyResourcePlanning.Services.Data.Calendar;
    using MyResourcePlanning.Services.Data.Project;
    using MyResourcePlanning.Services.Data.Request;
    using MyResourcePlanning.Services.Data.Skill;
    using MyResourcePlanning.Services.Data.SkillCategory;
    using MyResourcePlanning.Services.Data.Training;
    using MyResourcePlanning.Services.Data.User;
    using MyResourcePlanning.Services.Mapping;
    using MyResourcePlanning.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment currentEnv;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.configuration = configuration;
            this.currentEnv = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (this.currentEnv.EnvironmentName == "Integration")
            {
                services.AddDbContext<MyResourcePlanningDbContext>(
                    options => options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            }
            else
            {
                services.AddDbContext<MyResourcePlanningDbContext>(
                    options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));
            }

            services
                .AddIdentity<User, UserRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<MyResourcePlanningDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            services
                .AddMvc(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });

            services.AddSingleton(this.configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<ISkillCategoryService, SkillCategoryService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<ICalendarService, CalendarService>();
            services.AddTransient<IAdminService, AdminService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var cultureInfo = new CultureInfo("en-GB");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyResourcePlanningDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new MyResourcePlanningDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

namespace WriteIt.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WriteIt.Data;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using WriteIt.Web.Areas.Identity.Services;
    using WriteIt.Models;
    using WriteIt.Utilities.Constants;
    using WriteIt.Web.Controllers.Common;
    using WriteIt.Services.Admin;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Services.User;
    using WriteIt.Services.Interfaces.User;
    using WriteIt.Services.Interfaces.Moderator;
    using WriteIt.Services.Moderator;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //TODO
        //Add Comment(controller)
        //Give karma - comments(page)
        //unit tests
        //finished

        //validation
        //order the instances,posts and comments by most karma
        //add top button for player to see his posts and comments
        //details,edit and delete for posts and comments

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WriteItContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(WebConstants.AppName)));

            services
              .AddIdentity<User, IdentityRole>()
              .AddDefaultUI()
              .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<WriteItContext>();

            //services.AddAuthentication()
            //    .AddFacebook(options =>
            //    {
            //        options.AppId = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
            //        options.AppSecret = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
            //    })
            //   .AddGoogle(options =>
            //   {
            //       options.ClientId = this.Configuration.GetSection("ExternalAuthentication:Google:ClientId").Value;
            //       options.ClientSecret = this.Configuration.GetSection("ExternalAuthentication:Google:ClientSecret").Value;
            //   })
            //    .AddGitHub(options =>
            //    {
            //        options.ClientId = this.Configuration.GetSection("ExternalAuthentication:GitHub:ClientId").Value;
            //        options.ClientSecret = this.Configuration.GetSection("ExternalAuthentication:GitHub:ClientSecret").Value;
            //    });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = ValidationConstants.MinPasswordLength,
                    RequiredUniqueChars = ValidationConstants.MinPasswordRequiredUniqueChars,
                    RequireLowercase = ValidationConstants.PasswordRequireLowercase,
                    RequireDigit = ValidationConstants.PasswordRequireDigit,
                    RequireUppercase = ValidationConstants.PasswordRequireUppercase,
                    RequireNonAlphanumeric = ValidationConstants.PasswordRequireNonAlphanumeric
                };

                // options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddAutoMapper();

            services.AddSingleton<IEmailSender, SendGridEmailSender>();
            services.Configure<SendGridOptions>(this.Configuration.GetSection("EmailSettings"));

            AddServices(services);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminThreadsService, AdminThreadsService>();
            services.AddScoped<IUserThreadsService, UserThreadsService>();
            services.AddScoped<IUserThreadInstancesService, UserThreadInstancesService>();
            services.AddScoped<IUserPostService, UserPostService>();
            services.AddScoped<IUserCommentService, UserCommentService>();
            services.AddScoped<IModeratorThreadsService, ModeratorThreadsService>();
            services.AddScoped<IModeratorThreadInstancesService, ModeratorThreadInstancesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            if (env.IsDevelopment())
            {
                app.SeedDatabase();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
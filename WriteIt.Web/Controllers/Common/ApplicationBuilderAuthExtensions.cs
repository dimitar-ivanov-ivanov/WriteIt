namespace WriteIt.Web.Controllers.Common
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using WriteIt.Models;
    using WriteIt.Utilities.Constants;

    public static class ApplicationBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "admin123";
        private const string DefaultModeratorPassword = "moderator123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Moderator")
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var admin = await userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    admin = new User()
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        FullName = "Admin Admin Admin",
                        DateOfRegistry = DateTime.Now,
                        Karma = 0
                    };

                    await userManager.CreateAsync(admin, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(admin, roles[0].Name);
                }

                var moderator = await userManager.FindByNameAsync("moderator");

                if (moderator == null)
                {
                    moderator = new User()
                    {
                        UserName = "moderator",
                        Email = "moderator@example.com",
                        FullName = "Moderator Moderator Moderator",
                        DateOfRegistry = DateTime.Now,
                        Karma = 0
                    };

                    await userManager.CreateAsync(moderator, DefaultModeratorPassword);
                    await userManager.AddToRoleAsync(moderator, roles[1].Name);
                }
            }
        }
    }
}
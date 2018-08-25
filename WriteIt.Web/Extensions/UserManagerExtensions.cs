namespace WriteIt.Web.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;
    using WriteIt.Models;

    public static class UserManagerExtensions
    {
        public static int GetKarma(this UserManager<User> userManager, User user)
        {
            return user.Karma;
        }

        public static string GetFullName(this UserManager<User> userManager, User user)
        {
            return user.FullName;
        }

        public static DateTime GetDateOfRegistry(this UserManager<User> userManager, User user)
        {
            return user.DateOfRegistry;
        }
    }
}
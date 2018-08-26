namespace WriteIt.Services.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using WriteIt.Common.Admin.ViewModels;
    using WriteIt.Data;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Utilities.Constants;

    public class AdminUserService : BaseService, IAdminUserService
    {
        private readonly UserManager<User> userManager;

        public AdminUserService(WriteItContext context, IMapper mapper, UserManager<User> userManager)
            : base(context, mapper)
        {
            this.userManager = userManager;
        }

        public async Task<UserDetailsViewModel> GetUserDetailsAsync(string id)
        {
            var user = await this.GetUser(id);

            if (user == null)
            {
                return null;
            }

            var roles = await this.userManager.GetRolesAsync(user);
            var model = this.Mapper.Map<UserDetailsViewModel>(user);
            model.Roles = roles;

            return model;
        }

        public async Task<IEnumerable<UserConciseViewModel>> GetUsersAsync(string currentUserId)
        {
            var users = await this.Context
                                       .Users
                                       .Where(u => u.Id != currentUserId)
                                       .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<UserConciseViewModel>>(users);

            foreach (var userConcise in model)
            {
                var userRole = await this.Context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userConcise.Id);

                if (userRole != null)
                {
                    var role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);

                    if (role.Name == WebConstants.ModeratorRole)
                    {
                        userConcise.IsModerator = true;
                    }
                    else if (role.Name == WebConstants.AdministratorRole)
                    {
                        userConcise.IsAdmin = true;
                    }
                }
            }

            return model;
        }

        public async Task<User> MakeModerator(string id)
        {
            var user = await this.GetUser(id);

            if (user == null)
            {
                return null;
            }

            var role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Name == WebConstants.ModeratorRole);

            this.Context.UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            this.Context.SaveChanges();

            return user;
        }

        //public async Task<UserDeleteViewModel> GetUserToDeleteAsync(string id)
        //{
        //    var user = await this.GetUser(id);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var model = this.Mapper.Map<UserDeleteViewModel>(user);

        //    return model;
        //}

        //public async Task DeleteUserAsync(UserDeleteViewModel model)
        //{
        //    var user = await GetUser(model.Id);

        //    await this.userManager.DeleteAsync(user);
        //}

        private async Task<User> GetUser(string id)
        {
            var user = await this.Context
               .Users
               .FindAsync(id);

            return user;
        }
    }
}
namespace WriteIt.Services.Interfaces.Admin
{
    using WriteIt.Common.Admin.ViewModels;
    using WriteIt.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<UserConciseViewModel>> GetUsersAsync(string id);

        Task<UserDetailsViewModel> GetUserDetailsAsync(string id);

        Task<User> MakeModerator(string id);

        Task<UserDeleteViewModel> GetUserToDeleteAsync(string id);

        Task DeleteUserAsync(UserDeleteViewModel model);
    }
}

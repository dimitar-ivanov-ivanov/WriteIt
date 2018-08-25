namespace WriteIt.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WriteIt.Common.Admin.ViewModels;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Web.Extensions;
    using WriteIt.Web.Helpers.Messages;

    public class UsersController : AdminController
    {
        private UserManager<User> userManager;
        private IAdminUserService userService;

        public UsersController(UserManager<User> userManager, IAdminUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.GetCurrentUser();

            var model = await this.userService.GetUsersAsync(currentUser.Id);

            return View(model);
        }

        public async Task<IActionResult> MakeModerator(string id)
        {
            var currentUser = await GetCurrentUser();

            if (currentUser.Id == id)
            {
                return Unauthorized();
            }

            var user = await this.userService.MakeModerator(id);

            if (user == null)
            {
                return NotFound();
            }

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Info,
                Message = "User became moderator successfully"
            });

            return RedirectToAction("/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUser = await GetCurrentUser();

            if (currentUser.Id == id)
            {
                return Unauthorized();
            }

            var model = await this.userService.GetUserToDeleteAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteViewModel model)
        {
            var currentUser = await GetCurrentUser();

            if (currentUser.Id == model.Id)
            {
                return Unauthorized();
            }

            await this.userService.DeleteUserAsync(model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Danger,
                Message = "User deleted successfully"
            });

            return RedirectToAction("/Index");
        }

        private async Task<User> GetCurrentUser()
        {
            return await this.userManager
                                    .GetUserAsync(this.User);
        }
    }
}
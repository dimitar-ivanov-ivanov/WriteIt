namespace WriteIt.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.User;

    public class ThreadInstancesController : Controller
    {
        private IUserThreadInstancesService instancesService;
        private UserManager<User> userManager;

        public ThreadInstancesController(IUserThreadInstancesService instancesService, UserManager<User> userManager)
        {
            this.instancesService = instancesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var model = await this.instancesService.GetInstancesAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var isUserAuthenticated = this.HttpContext.User.Identity.IsAuthenticated;
            string userId = GeUserId();

            foreach (var item in model)
            {
                item.IsUserAuthenticated = isUserAuthenticated;
                item.UserId = userId;

                var isUserSubscribedToInstance = await this.instancesService.CheckUserSubscriptionAsync(userId, item.Id);

                item.IsUserSubscribed = isUserSubscribedToInstance;
            }

            return View(model);
        }

        public async Task<IActionResult> Subscribe(int id)
        {
            var userId = this.GeUserId();
            var instanceThreadId = (await this.instancesService.GetInstanceAsync(id)).ThreadId;

            await this.instancesService.SubscribeUsersAsync(userId, id);

            return RedirectToAction("Index", new { id = instanceThreadId });
        }

        public async Task<IActionResult> Unsubscribe(int id)
        {
            var userId = this.GeUserId();
            var instanceThreadId = (await this.instancesService.GetInstanceAsync(id)).ThreadId;

            await this.instancesService.UnsubscribeUsersAsync(userId, id);

            return RedirectToAction("Index", new { id = instanceThreadId });
        }

        public async Task<IActionResult> GetSubscriptions()
        {
            var userId = this.GeUserId();

            var model = await this.instancesService.GetSubscriptions(userId);

            return View(model);
        }

        private string GeUserId()
        {
            return userManager.GetUserId(this.User);
        }
    }
}
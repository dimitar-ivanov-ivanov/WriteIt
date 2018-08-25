namespace WriteIt.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WriteIt.Common.User.BindingModels;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.User;
    using WriteIt.Web.Extensions;
    using WriteIt.Web.Helpers.Messages;

    public class PostsController : Controller
    {
        private IUserPostService postService;
        private UserManager<User> userManager;
        private IUserThreadInstancesService instancesService;

        public PostsController(IUserPostService postService, UserManager<User> userManager, IUserThreadInstancesService instancesService)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.instancesService = instancesService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var instance = await this.instancesService.GetInstanceAsync(id);

            if (instance == null)
            {
                return NotFound();
            }

            var model = new PostCreationBindingModel()
            {
                ThreadInstanceId = id,
                ThreadInstanceName = instance.Name.Substring(2, instance.Name.Length - 2)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostCreationBindingModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.postService.CreatePostAsync(userId, model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Info,
                Message = "Post created successfully"
            });

            return RedirectToPage("/Posts/Index", new { id = model.ThreadInstanceId });
        }
    }
}
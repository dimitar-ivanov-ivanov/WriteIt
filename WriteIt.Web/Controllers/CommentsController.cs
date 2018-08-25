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

    public class CommentsController : Controller
    {
        private IUserPostService postService;
        private IUserCommentService commentService;
        private UserManager<User> userManager;

        public CommentsController(IUserPostService postService, IUserCommentService commentService,UserManager<User> userManager)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var post = await this.postService.GetPostAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var model = new CommentCreationBindingModel()
            {
                PostId = post.Id,
                PostName = post.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentCreationBindingModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.commentService.CreateCommentAsync(userId, model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Info,
                Message = "Comment created successfully"
            });

            return RedirectToPage("/Comments/All", new { id = model.PostId });
        }
    }
}
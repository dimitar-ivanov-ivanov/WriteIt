namespace WriteIt.Web.Pages.Comments
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Services.Interfaces.User;

    public class AllModel : PageModel
    {
        private IUserCommentService commentService;
        private IUserPostService postService;

        public AllModel(IUserCommentService commentService, IUserPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        public int PostId { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public string PostName { get; set; }

        public IEnumerable<CommentConciseViewModel> Comments { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var model = await this.commentService.GetCommentsAsync(id);

            if(model == null)
            {
                return NotFound();
            }

            this.Comments = model;
            var post = await this.postService.GetPostAsync(id);

            this.PostId = post.Id;
            this.PostName = post.Name;
            this.IsUserAuthenticated = this.User.Identity.IsAuthenticated;

            return Page();
        }

        public async Task<IActionResult> OnGetGiveKarma(int postId, int id)
        {
            await this.commentService.GiveCommentKarmaAsync(id);

            return RedirectToPage("/Comments/All", new { id = postId });
        }
    }
}
namespace WriteIt.Web.Pages.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Services.Interfaces.User;

    public class IndexModel : PageModel
    {
        private IUserPostService postService;
        private IUserThreadInstancesService instancesService;

        public IndexModel(IUserPostService postService, IUserThreadInstancesService instancesService)
        {
            this.postService = postService;
            this.instancesService = instancesService;
        }

        public int ThreadInstanceId { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public string ThreadInstanceName { get; set; }

        public IEnumerable<PostConciseViewModel> Posts { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var model = await this.postService.GetPostsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            this.Posts = model;

            var instance = await instancesService.GetInstanceAsync(id);
            var instanceName = instance.Name;
            instanceName = instanceName.Substring(2, instanceName.Length - 2);

            this.ThreadInstanceName = instanceName;
            this.ThreadInstanceId = id;

            if (this.User != null)
            {
                this.IsUserAuthenticated = this.HttpContext.User.Identity.IsAuthenticated;
            }

            return Page();
        }

        public async Task<IActionResult> OnGetGiveKarma(int instanceId, int id)
        {
            await this.postService.GivePostKarmaAsync(id);

            return RedirectToPage("/Posts/Index", new { id = instanceId });
        }
    }
}
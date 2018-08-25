namespace WriteIt.Web.Pages.Posts
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Threading.Tasks;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Services.Interfaces.User;

    public class DetailsModel : PageModel
    {
        private IUserPostService postService;
        private IUserThreadInstancesService instancesService;

        public DetailsModel(IUserPostService postService, IUserThreadInstancesService instancesService)
        {
            this.postService = postService;
            this.instancesService = instancesService;
        }

        public int ThreadInstanceId { get; set; }

        public PostDetailsViewModel Post { get; set; }

        public async Task<IActionResult> OnGet(int instanceId, int id)
        {
            var model = await this.postService.GetPostDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            this.Post = model;

            var instance = await this.instancesService.GetInstanceAsync(instanceId);
            this.ThreadInstanceId = instance.Id;

            return Page();
        }

        public async Task<IActionResult> OnGetGiveKarma(int instanceId, int id)
        {

            await this.postService.GivePostKarmaAsync(id);

            return RedirectToPage("/Posts/Details", new { instanceId = instanceId, id = id });
        }
    }
}
namespace WriteIt.Web.Areas.Moderator.Pages.ThreadInstances
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Threading.Tasks;
    using WriteIt.Common.Moderator.ViewModels;
    using WriteIt.Services.Interfaces.Moderator;

    public class DetailsModel : PageModel
    {
        private IModeratorThreadInstancesService instancesService;

        public DetailsModel(IModeratorThreadInstancesService instancesService)
        {
            this.instancesService = instancesService;
        }

        public ThreadInstanceDetailsViewModel ThreadInstance { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var model = await this.instancesService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            this.ThreadInstance = model;

            return Page();
        }
    }
}
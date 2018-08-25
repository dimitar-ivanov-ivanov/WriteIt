namespace WriteIt.Web.Areas.Admin.Pages.Users
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using WriteIt.Common.Admin.ViewModels;
    using WriteIt.Services.Interfaces.Admin;

    public class DetailsModel : PageModel
    {
        private IAdminUserService userService;

        public DetailsModel(IAdminUserService userService)
        {
            this.userService = userService;
        }

        public UserDetailsViewModel UserDetails { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            var model = await this.userService.GetUserDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            this.UserDetails = model;

            return Page();
        }
    }
}
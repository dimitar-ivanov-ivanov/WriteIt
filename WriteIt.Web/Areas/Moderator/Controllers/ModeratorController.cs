namespace WriteIt.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WriteIt.Utilities.Constants;

    [Area(WebConstants.ModeratorArea)]
    [Authorize(Roles = WebConstants.AdministratorRole + ", " + WebConstants.ModeratorRole)]
    public abstract class ModeratorController : Controller
    {
    }
}

namespace WriteIt.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ModeratorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
namespace WriteIt.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WriteIt.Services.Interfaces.User;
    using WriteIt.Web.Models;

    public class HomeController : Controller
    {
        private IUserThreadsService threadsService;

        public HomeController(IUserThreadsService threadsService)
        {
            this.threadsService = threadsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.threadsService.GetThreadsAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
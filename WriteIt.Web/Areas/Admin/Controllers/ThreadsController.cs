namespace WriteIt.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WriteIt.Common.Admin.BindingModels;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Web.Extensions;
    using WriteIt.Web.Helpers.Messages;

    public class ThreadsController : AdminController
    {
        private IAdminThreadsService threadsService;

        public ThreadsController(IAdminThreadsService threadsService)
        {
            this.threadsService = threadsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThreadCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var thread = await this.threadsService.AddThreadAsync(model);

            if (this.TempData != null)
            {
                this.TempData.Put("__Message", new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Thread created successfully"
                });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
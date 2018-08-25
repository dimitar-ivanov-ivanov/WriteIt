namespace WriteIt.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WriteIt.Common.Moderator.BindingModels;
    using WriteIt.Common.Moderator.ViewModels;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.Moderator;
    using WriteIt.Web.Extensions;
    using WriteIt.Web.Helpers.Messages;

    public class ThreadInstancesController : ModeratorController
    {
        private UserManager<User> userManager;
        private IModeratorThreadsService threadsService;
        private IModeratorThreadInstancesService instancesService;

        public ThreadInstancesController(IModeratorThreadsService threadsService, IModeratorThreadInstancesService instancesService, UserManager<User> userManager)
        {
            this.threadsService = threadsService;
            this.instancesService = instancesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.GetCurrentUser();

            var model = await this.instancesService.GetThreadInstancesAsync(currentUser.Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var threads = await this.threadsService.GetThreadsAsync();
            IEnumerable<SelectListItem> selectList = new List<SelectListItem>();

            if (threads != null)
            {
                selectList = threads.Select(t => new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                });
            }

            var model = new ThreadInstanceCreationBindingModel()
            {
                Threads = selectList
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThreadInstanceCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var threadId = int.Parse(model.ThreadId);
            model.CreatorId = (await this.GetCurrentUser()).Id;
            model.ThreadName = (await this.threadsService.GetThreadAsync(threadId)).Name;

            await this.instancesService.CreateThreadInstanceAsync(model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Thread instance created successfully"
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await this.instancesService.GetInstanceToDeleteAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ThreadInstanceDeleteViewModel model)
        {
            await this.instancesService.DeleteInstanceAsync(model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Danger,
                Message = "Thread instance deleted successfully"
            });

            return RedirectToAction("/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.instancesService.GetInstanceToEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ThreadInstanceEditBindingModel model)
        {
            await this.instancesService.EditInstanceAsync(model);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Warning,
                Message = "Thread instance edited successfully"
            });

            return RedirectToAction("/Index");
        }

        private async Task<User> GetCurrentUser()
        {
            return await this.userManager
                                    .GetUserAsync(this.User);
        }
    }
}
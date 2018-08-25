namespace WriteIt.Services.Moderator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using WriteIt.Common.Moderator.BindingModels;
    using WriteIt.Common.Moderator.ViewModels;
    using WriteIt.Data;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.Moderator;

    public class ModeratorThreadInstancesService : BaseService, IModeratorThreadInstancesService
    {
        public ModeratorThreadInstancesService(WriteItContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task CreateThreadInstanceAsync(ThreadInstanceCreationBindingModel model)
        {
            model.DateOfRegistry = DateTime.Now;
            model.Name = model.ThreadName[0].ToString().ToLower() + "/" + model.Name;

            var instance = this.Mapper.Map<ThreadInstance>(model);

            await this.Context.ThreadInstances.AddAsync(instance);

            this.Context.SaveChanges();
        }

        public async Task<ThreadInstanceDetailsViewModel> GetDetailsAsync(int id)
        {
            var instance = await this.Context
                .ThreadInstances
                .Include(ti => ti.Creator)
                .Include(ti => ti.Posts)
                .FirstOrDefaultAsync(ti => ti.Id == id);

            if (instance == null)
            {
                return null;
            }

            var model = this.Mapper.Map<ThreadInstanceDetailsViewModel>(instance);

            return model;
        }

        public async Task<ThreadInstanceDeleteViewModel> GetInstanceToDeleteAsync(int id)
        {
            var instance = await GetInstanceAsync(id);

            if (instance == null)
            {
                return null;
            }

            var model = this.Mapper.Map<ThreadInstanceDeleteViewModel>(instance);

            return model;
        }

        public async Task DeleteInstanceAsync(ThreadInstanceDeleteViewModel model)
        {
            var instance = await this.GetInstanceAsync(model.Id);

            this.Context.ThreadInstances.Remove(instance);

            this.Context.SaveChanges();
        }

        public async Task<ThreadInstanceEditBindingModel> GetInstanceToEditAsync(int id)
        {
            var instance = await GetInstanceAsync(id);

            if (instance == null)
            {
                return null;
            }

            var model = this.Mapper.Map<ThreadInstanceEditBindingModel>(instance);

            return model;
        }

        public async Task EditInstanceAsync(ThreadInstanceEditBindingModel model)
        {
            var instance = await this.GetInstanceAsync(model.Id);

            instance.Name = model.ThreadName[0].ToString().ToLower() + "/" + model.Name;
            instance.Description = model.Description;

            this.Context.SaveChanges();
        }

        private async Task<ThreadInstance> GetInstanceAsync(int id)
        {
            return await this.Context.ThreadInstances
                .Include(ti => ti.Thread)
                .Include(ti => ti.Creator)
                .FirstOrDefaultAsync(ti => ti.Id == id);
        }

        public async Task<IEnumerable<ThreadInstanceConciseViewModel>> GetThreadInstancesAsync(string creatorId)
        {
            var instance = await this.Context
                               .ThreadInstances
                               .Include(ti => ti.Creator)
                               .Where(ti => ti.CreatorId == creatorId)
                               .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<ThreadInstanceConciseViewModel>>(instance);

            return model;
        }
    }
}
namespace WriteIt.Services.User
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Data;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.User;

    public class UserThreadInstancesService : BaseService, IUserThreadInstancesService
    {
        public UserThreadInstancesService(WriteItContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<ThreadInstanceConciseViewModel>> GetInstancesAsync(int id)
        {
            var instance = await this.Context
                          .ThreadInstances
                          .Include(ti => ti.Posts)
                          .Include(t => t.Creator)
                          .Include(t => t.Thread)
                          .Where(t => t.ThreadId == id)
                          .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<ThreadInstanceConciseViewModel>>(instance);

            return model;
        }

        public async Task<bool> CheckUserSubscriptionAsync(string userId, int instanceId)
        {
            var subscribed = await this.Context
                .UserThreadInstances
                .FirstOrDefaultAsync(uti => uti.SubscriberId == userId && uti.ThreadInstanceId == instanceId);

            return subscribed != null;
        }

        public async Task SubscribeUsersAsync(string userId, int instanceId)
        {
            var isSubscribed = await this.CheckUserSubscriptionAsync(userId, instanceId);

            if (!isSubscribed)
            {
                this.Context.UserThreadInstances.Add(new Models.UserThreadInstance()
                {
                    SubscriberId = userId,
                    ThreadInstanceId = instanceId
                });

                this.Context.SaveChanges();
            }
        }

        public async Task UnsubscribeUsersAsync(string userId, int instanceId)
        {
            var isSubscribed = await this.CheckUserSubscriptionAsync(userId, instanceId);

            if (isSubscribed)
            {
                var subscription = this.Context.UserThreadInstances
                    .Where(uti => uti.SubscriberId == userId && uti.ThreadInstanceId == instanceId)
                    .FirstOrDefault();

                this.Context.UserThreadInstances.Remove(subscription);

                this.Context.SaveChanges();
            }
        }

        public async Task<ThreadInstance> GetInstanceAsync(int id)
        {
            var instance = await this.Context
                            .ThreadInstances
                            .Include(ti => ti.Thread)
                            .FirstOrDefaultAsync(ti => ti.Id == id);

            return instance;
        }

        public async Task<IEnumerable<ThreadInstanceSubscriptionViewModel>> GetSubscriptions(string userId)
        {
            var subscriptions = await this.Context
                            .UserThreadInstances
                            .Where(uti => uti.SubscriberId == userId)
                            .Select(uti => uti.ThreadInstance)
                            .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<ThreadInstanceSubscriptionViewModel>>(subscriptions);

            return model;
        }
    }
}
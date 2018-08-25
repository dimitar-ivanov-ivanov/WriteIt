namespace WriteIt.Services.Interfaces.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Models;

    public interface IUserThreadInstancesService
    {
        Task<IEnumerable<ThreadInstanceConciseViewModel>> GetInstancesAsync(int id);

        Task<ThreadInstance> GetInstanceAsync(int id);

        Task<bool> CheckUserSubscriptionAsync(string userId, int instanceId);

        Task SubscribeUsersAsync(string userId, int instanceId);

        Task UnsubscribeUsersAsync(string userId, int instanceId);

        Task<IEnumerable<ThreadInstanceSubscriptionViewModel>> GetSubscriptions(string userId);
    }
}
namespace WriteIt.Services.Interfaces.Moderator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Common.Moderator.BindingModels;
    using WriteIt.Common.Moderator.ViewModels;

    public interface IModeratorThreadInstancesService
    {
        Task CreateThreadInstanceAsync(ThreadInstanceCreationBindingModel model);

        Task<IEnumerable<ThreadInstanceConciseViewModel>> GetThreadInstancesAsync(string creatorId);

        Task<ThreadInstanceDetailsViewModel> GetDetailsAsync(int id);

        Task<ThreadInstanceDeleteViewModel> GetInstanceToDeleteAsync(int id);

        Task DeleteInstanceAsync(ThreadInstanceDeleteViewModel model);

        Task<ThreadInstanceEditBindingModel> GetInstanceToEditAsync(int id);

        Task EditInstanceAsync(ThreadInstanceEditBindingModel model);
    }
}
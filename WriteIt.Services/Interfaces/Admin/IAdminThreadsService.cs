namespace WriteIt.Services.Interfaces.Admin
{
    using System.Threading.Tasks;
    using WriteIt.Common.Admin.BindingModels;
    using WriteIt.Models;

    public interface IAdminThreadsService
    {
        Task<Thread> AddThreadAsync(ThreadCreationBindingModel model);
    }
}
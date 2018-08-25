namespace WriteIt.Services.Interfaces.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Models;

    public interface IUserThreadsService
    {
        Task<IEnumerable<ThreadConciseViewModel>> GetThreadsAsync();
    }
}

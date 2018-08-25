namespace WriteIt.Services.Interfaces.Moderator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Models;

    public interface IModeratorThreadsService
    {
        Task<Thread> GetThreadAsync(int id);

        Task<IEnumerable<Thread>> GetThreadsAsync();
    }
}
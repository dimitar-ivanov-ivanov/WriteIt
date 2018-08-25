namespace WriteIt.Services.Moderator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using WriteIt.Data;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.Moderator;

    public class ModeratorThreadsService : BaseService, IModeratorThreadsService
    {
        public ModeratorThreadsService(WriteItContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<Thread> GetThreadAsync(int id)
        {
            var thread = await this.Context.Threads.FirstOrDefaultAsync(t => t.Id == id);

            return thread;
        }

        public async Task<IEnumerable<Thread>> GetThreadsAsync()
        {
            var threads = await this.Context.Threads.ToListAsync();

            return threads;
        }
    }
}
namespace WriteIt.Services.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Data;
    using WriteIt.Services.Interfaces.User;

    public class UserThreadsService : BaseService, IUserThreadsService
    {
        public UserThreadsService(WriteItContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<ThreadConciseViewModel>> GetThreadsAsync()
        {
            var threads = await this.Context
                .Threads
                .Include(t => t.Instances)
                .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<ThreadConciseViewModel>>(threads);

            return model;
        }
    }
}

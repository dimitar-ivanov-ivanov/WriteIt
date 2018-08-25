namespace WriteIt.Services.Admin
{
    using System.Threading.Tasks;
    using AutoMapper;
    using WriteIt.Common.Admin.BindingModels;
    using WriteIt.Data;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Utilities.Constants;
    using WriteIt.Utilities.Validation;

    public class AdminThreadsService : BaseService, IAdminThreadsService
    {
        public AdminThreadsService(WriteItContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Models.Thread> AddThreadAsync(ThreadCreationBindingModel model)
        {
            Validator.EnsureNotNull(model, string.Format(ErrorMessages.NoValue, WebConstants.Thread));
            Validator.EnsureStringNotNullOrEmpty(model.Name, string.Format(ErrorMessages.NoName, WebConstants.Thread));
            Validator.EnsureStringNotNullOrEmpty(model.Description, string.Format(ErrorMessages.NoDescription, WebConstants.Thread));

            var thread = this.Mapper.Map<Models.Thread>(model);
            await this.Context.Threads.AddAsync(thread);
            this.Context.SaveChanges();

            return thread;
        }
    }
}

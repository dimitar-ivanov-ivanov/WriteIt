namespace WriteIt.Services
{
    using AutoMapper;
    using WriteIt.Data;

    public abstract class BaseService
    {
        protected BaseService(WriteItContext context,
             IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }


        protected WriteItContext Context { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}

namespace WriteIt.Tests.Mocks
{
    using AutoMapper;
    using WriteIt.Web.Mapping;

    public class MockAutoMapper
    {
        static MockAutoMapper()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
        }

        public static IMapper GetAutoMapper()
        {
            return Mapper.Instance;
        }
    }
}
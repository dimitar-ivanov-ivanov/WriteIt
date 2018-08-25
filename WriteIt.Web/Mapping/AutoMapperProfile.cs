namespace WriteIt.Web.Mapping
{
    using AutoMapper;
    using System.Linq;
    using WriteIt.Common.Admin.BindingModels;
    using WriteIt.Common.Admin.ViewModels;
    using WriteIt.Common.Moderator.BindingModels;
    using WriteIt.Common.Moderator.ViewModels;
    using WriteIt.Common.User.BindingModels;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateUserAdminBindings();
            CreateThreadAdminBindings();
            CreateThreadInstancesBindings();
            CreatePostsBindings();
            CreateCommentBindings();
        }

        private void CreateUserAdminBindings()
        {
            this.CreateMap<User, UserConciseViewModel>();
            this.CreateMap<User, UserDetailsViewModel>();
            this.CreateMap<User, UserDeleteViewModel>()
                .ForMember(dest => dest.DateOfRegistry, opt => opt.MapFrom(a => a.DateOfRegistry.ToString()));
        }

        private void CreateThreadAdminBindings()
        {
            this.CreateMap<ThreadCreationBindingModel, Thread>();
            this.CreateMap<Thread, ThreadConciseViewModel>()
                .ForMember(dest => dest.InstancesCount, opt => opt.MapFrom(a => a.Instances.Count));
        }

        private void CreateThreadInstancesBindings()
        {
            this.CreateMap<ThreadInstanceCreationBindingModel, ThreadInstance>();
            this.CreateMap<ThreadInstance, Common.User.ViewModels.ThreadInstanceConciseViewModel>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(a => a.Creator.UserName))
                .ForMember(dest => dest.PostsCount, opt => opt.MapFrom(a => a.Posts.Count))
                .ForMember(dest => dest.TotalKarma, opt => opt.MapFrom(a => a.Posts.Sum(p => p.Karma)))
                .ForMember(dest => dest.DateOfRegistry, opt => opt.MapFrom(a => a.DateOfRegistry.ToString()));

            this.CreateMap<Common.Moderator.ViewModels.ThreadInstanceConciseViewModel, ThreadInstance>();

            this.CreateMap<ThreadInstance, ThreadInstanceDetailsViewModel>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(a => a.Creator.UserName))
                .ForMember(dest => dest.PostsCount, opt => opt.MapFrom(a => a.Posts.Count))
                .ForMember(dest => dest.TotalKarma, opt => opt.MapFrom(a => a.Posts.Sum(p => p.Karma)));

            this.CreateMap<ThreadInstance, ThreadInstanceDeleteViewModel>();
            this.CreateMap<ThreadInstance, ThreadInstanceEditBindingModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(a => a.Name.Substring(2, a.Name.Length - 2)));

            this.CreateMap<ThreadInstance, Common.User.ViewModels.ThreadInstanceSubscriptionViewModel>();
        }

        private void CreatePostsBindings()
        {
            this.CreateMap<Post, PostConciseViewModel>()
                 .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(a => a.Comments.Count))
                 .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(a => a.Creator.UserName));

            this.CreateMap<PostCreationBindingModel, Post>();

            this.CreateMap<Post, PostDetailsViewModel>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(a => a.Creator.UserName))
                .ForMember(dest => dest.ThreadInstanceName, opt => opt.MapFrom(a => a.ThreadInstance.Name));
        }

        private void CreateCommentBindings()
        {
            this.CreateMap<Comment, CommentConciseViewModel>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(a => a.Creator.UserName));

            this.CreateMap<CommentCreationBindingModel, Comment>();
        }
    }
}
namespace WriteIt.Services.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using WriteIt.Common.User.BindingModels;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Data;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.User;

    public class UserPostService : BaseService, IUserPostService
    {
        public UserPostService(WriteItContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task CreatePostAsync(string creatorId, PostCreationBindingModel model)
        {
            var post = this.Mapper.Map<Post>(model);
            post.CreatorId = creatorId;

            await this.Context.Posts.AddAsync(post);

            this.Context.SaveChanges();
        }

        public async Task<PostDetailsViewModel> GetPostDetailsAsync(int id)
        {
            var post = await this.GetPostAsync(id);

            var model = this.Mapper.Map<PostDetailsViewModel>(post);

            return model;
        }

        public async Task<IEnumerable<PostConciseViewModel>> GetPostsAsync(int threadInstanceId)
        {
            var posts = await this.Context
                        .Posts
                        .Include(p => p.Creator)
                        .Include(p => p.Comments)
                        .Where(p => p.ThreadInstanceId == threadInstanceId)
                        .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<PostConciseViewModel>>(posts);

            return model;
        }

        public async Task GivePostKarmaAsync(int id)
        {
            var post = await this.GetPostAsync(id);

            post.Karma++;
            post.Creator.Karma++;

            this.Context.SaveChanges();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await this.Context
                .Posts
                .Include(p => p.Creator)
                .Include(p => p.ThreadInstance)
                .FirstOrDefaultAsync(p => p.Id == id);

            return post;
        }
    }
}
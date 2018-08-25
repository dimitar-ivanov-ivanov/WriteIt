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

    public class UserCommentService : BaseService, IUserCommentService
    {
        public UserCommentService(WriteItContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task CreateCommentAsync(string creatorId, CommentCreationBindingModel model)
        {
            var comment = this.Mapper.Map<Comment>(model);
            comment.CreatorId = creatorId;

            await this.Context.Comments.AddAsync(comment);

            this.Context.SaveChanges();
        }

        public async Task<IEnumerable<CommentConciseViewModel>> GetCommentsAsync(int postId)
        {
            var comments = await this.Context
                                   .Comments
                                   .Include(c => c.Creator)
                                   .Include(c => c.Post)
                                   .Where(c => c.PostId == postId)
                                   .ToListAsync();

            var model = this.Mapper.Map<IEnumerable<CommentConciseViewModel>>(comments);

            return model;
        }

        public async Task GiveCommentKarmaAsync(int id)
        {
            var comment = await this.GetCommentAsync(id);

            comment.Karma++;
            comment.Creator.Karma++;

            this.Context.SaveChanges();
        }

        private async Task<Comment> GetCommentAsync(int id)
        {
            var comment = await this.Context
                .Comments
                .Include(p => p.Creator)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(p => p.Id == id);

            return comment;
        }
    }
}

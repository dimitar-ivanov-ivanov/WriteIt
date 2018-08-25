namespace WriteIt.Services.Interfaces.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Common.User.BindingModels;
    using WriteIt.Common.User.ViewModels;

    public interface IUserCommentService
    {
        Task<IEnumerable<CommentConciseViewModel>> GetCommentsAsync(int postId);

        Task CreateCommentAsync(string creatorId, CommentCreationBindingModel model);

        Task GiveCommentKarmaAsync(int id);
    }
}

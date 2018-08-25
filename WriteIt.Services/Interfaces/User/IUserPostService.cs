namespace WriteIt.Services.Interfaces.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WriteIt.Common.User.BindingModels;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Models;

    public interface IUserPostService
    {
        Task<IEnumerable<PostConciseViewModel>> GetPostsAsync(int threadInstanceId);

        Task CreatePostAsync(string creatorId,PostCreationBindingModel model);

        Task GivePostKarmaAsync(int id);

        Task<PostDetailsViewModel> GetPostDetailsAsync(int id);

        Task<Post> GetPostAsync(int id);
    }
}
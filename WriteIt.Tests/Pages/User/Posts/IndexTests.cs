namespace WriteIt.Tests.Pages.User.Posts
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WriteIt.Common.User.ViewModels;
    using WriteIt.Data;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.User;
    using WriteIt.Tests.Mocks;
    using WriteIt.Web.Pages.Posts;

    [TestClass]
    public class IndexTests
    {
        private WriteItContext context;
        private IMapper mapper;

        [TestMethod]
        public async Task Index_ShouldReturnPostsAndThreadInstance()
        {
            //Assert
            var mockPostRepository = new Mock<IUserPostService>();
            var mockInstanceRepository = new Mock<IUserThreadInstancesService>();

            var methodPostCalled = false;
            var methodInstanceCalled = false;

            var instance = new ThreadInstance()
            {
                Id = 1,
                Name = "f/First"
            };

            var posts = new[]
            {
                new Post(){Id=1,Name ="First",ThreadInstanceId = instance.Id},
                new Post(){Id=2,Name ="Second",ThreadInstanceId = instance.Id},
                new Post(){Id=3,Name ="Third",ThreadInstanceId = instance.Id},
            };

            var models = new[]
            {
                new PostConciseViewModel(){Id=1,Name ="First" },
                new PostConciseViewModel(){Id=2,Name ="Second"},
                new PostConciseViewModel(){Id=3,Name ="Third"},
            };

            context.ThreadInstances.Add(instance);
            context.SaveChanges();

            context.Posts.AddRange(posts);
            context.SaveChanges();

            mockInstanceRepository
             .Setup(repo => repo.GetInstanceAsync(instance.Id))
                 .ReturnsAsync(instance)
                 .Callback(() => methodInstanceCalled = true);

            mockPostRepository
            .Setup(repo => repo.GetPostsAsync(instance.Id))
                .ReturnsAsync(models)
                .Callback(() => methodPostCalled = true);

            //Mock http context?
            var indexModel = new IndexModel(mockPostRepository.Object, mockInstanceRepository.Object);

            //Act
            var result = await indexModel.OnGet(instance.Id) as ViewResult;

            //Assert
            Assert.IsTrue(methodInstanceCalled);
            Assert.IsTrue(methodPostCalled);
            Assert.AreEqual(indexModel.ThreadInstanceId, instance.Id);
            Assert.AreEqual(indexModel.ThreadInstanceName, instance.Name.Substring(2, instance.Name.Length - 2));
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, indexModel.Posts.Select(u => u.Id).ToArray());
        }

        [TestInitialize]
        public void InitializeTests()
        {
            this.context = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
        }
    }
}

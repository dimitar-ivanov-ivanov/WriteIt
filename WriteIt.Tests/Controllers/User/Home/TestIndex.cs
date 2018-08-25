namespace WriteIt.Tests.Controllers.User.Home
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
    using WriteIt.Web.Controllers;

    [TestClass]
    public class TestIndex
    {
        private WriteItContext context;
        private IMapper mapper;

        [TestMethod]
        public async Task Index_ShouldReturnAllUsersExceptCurrent()
        {
            //Arrange
            var threads = new[]
            {
                new Thread(){Id = 1,Name="First"},
                new Thread(){Id = 2,Name="Second"},
                new Thread(){Id = 3,Name="Third"},
                new Thread(){Id = 4,Name="Fourth"},
            };

            var methodCalled = false;

            var models = new[]
            {
                new ThreadConciseViewModel() { Id = threads[0].Id, Name = threads[0].Name },
                new ThreadConciseViewModel() { Id = threads[1].Id, Name = threads[1].Name },
                new ThreadConciseViewModel() { Id = threads[2].Id, Name = threads[2].Name },
                new ThreadConciseViewModel() { Id = threads[3].Id, Name = threads[3].Name }
            };

            context.Threads.AddRange(threads);
            context.SaveChanges();

            var mockRepository = new Mock<IUserThreadsService>();
            mockRepository
                .Setup(repo => repo.GetThreadsAsync())
                    .ReturnsAsync(models)
                    .Callback(() => methodCalled = true);

            var controller = new HomeController(mockRepository.Object);

            //Act
            var result = await controller.Index() as ViewResult;

            //Assert
            Assert.IsTrue(methodCalled);
            var resultIds = result.Model as IEnumerable<ThreadConciseViewModel>;
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 },
                resultIds.Select(u => u.Id).ToArray());
        }

        [TestInitialize]
        public void InitializeTests()
        {
            this.context = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
        }
    }
}

namespace WriteIt.Tests.Controllers.Admin.ThreadController
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Threading.Tasks;
    using WriteIt.Common.Admin.BindingModels;
    using WriteIt.Models;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Web.Areas.Admin.Controllers;

    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        public async Task Create_WithValidCourse_ReturnsProperView()
        {
            //Arrange
            var thread = new Thread()
            {
                Name = "Random name",
                Description = "Random description"
            };

            var model = new ThreadCreationBindingModel()
            {
                Name = thread.Name,
                Description = thread.Description
            };

            var mockRepository = new Mock<IAdminThreadsService>();
            mockRepository
                .Setup(repo => repo.AddThreadAsync(model))
                .ReturnsAsync(thread);

            var controller = new ThreadsController(mockRepository.Object);

            //Act
            var result = await controller.Create(model);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Create_WithValidCourse_ShouldCallService()
        {
            //Arrange
            var serviceCalled = false;
            var model = new ThreadCreationBindingModel()
            {
                Name = "Random name",
                Description = "Random description"
            };

            var mockRepository = new Mock<IAdminThreadsService>();
            mockRepository
                .Setup(repo => repo.AddThreadAsync(model))
                .Callback(() => serviceCalled = true);

            var controller = new ThreadsController(mockRepository.Object);

            //Act
            var result = controller.Create(model);

            //Assert
            Assert.IsTrue(serviceCalled);
        }
    }
}
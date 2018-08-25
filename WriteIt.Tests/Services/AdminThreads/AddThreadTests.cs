namespace WriteIt.Tests.Services.AdminThreads
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using WriteIt.Common.Admin.BindingModels;
    using WriteIt.Data;
    using WriteIt.Services.Admin;
    using WriteIt.Services.Interfaces.Admin;
    using WriteIt.Tests.Mocks;
    using WriteIt.Utilities.Constants;

    [TestClass]
    public class AddThreadTests
    {
        private WriteItContext context;
        private IAdminThreadsService service;

        [TestMethod]
        public async Task AddThread_WithProperData_ShouldAddCorrectly()
        {
            //Arrange
            var name = "New thread name";
            var description = "New thread description";

            var threadModel = new ThreadCreationBindingModel()
            {
                Name = name,
                Description = description
            };

            //Act
            await this.service.AddThreadAsync(threadModel);

            //Assert
            Assert.AreEqual(1, this.context.Threads.Count());
            var course = this.context.Threads.First();
            Assert.AreEqual(course.Name, name);
            Assert.AreEqual(course.Description, description);
        }

        [TestMethod]
        public async Task AddCourse_WithMissingName_ShouldThrowException()
        {
            //Arrange
            ThreadCreationBindingModel threadModel = new ThreadCreationBindingModel()
            {
                Name = null,
                Description = "random description."
            };

            //Act
            Func<Task> addThread = () => this.service.AddThreadAsync(threadModel);

            //Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(addThread);
            Assert.AreEqual(exception.Message, string.Format(ErrorMessages.NoName, WebConstants.Thread));
        }

        [TestMethod]
        public async Task AddCourse_WithMissingDescription_ShouldThrowException()
        {
            //Arrange
            ThreadCreationBindingModel threadModel = new ThreadCreationBindingModel()
            {
                Name = "Random name",
                Description = null
            };

            //Act
            Func<Task> addThread = () => this.service.AddThreadAsync(threadModel);

            //Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(addThread);
            Assert.AreEqual(exception.Message, string.Format(ErrorMessages.NoDescription, WebConstants.Thread));
        }

        [TestInitialize]
        public void InitializeTests()
        {
            this.context = MockDbContext.GetContext();
            this.service = new AdminThreadsService(this.context, MockAutoMapper.GetAutoMapper());
        }
    }
}
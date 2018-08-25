namespace WriteIt.Tests.Controllers.Admin.ThreadController
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using WriteIt.Utilities.Constants;
    using WriteIt.Web.Areas.Admin.Controllers;

    [TestClass]
    public class AdminAccessTests
    {
        [TestMethod]
        public void ThreadsController_ShouldBeInAdminArea()
        {
            //Arrange
            var area = typeof(ThreadsController)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AreaAttribute)
                as AreaAttribute;

            //Assert
            Assert.IsNotNull(area);
            Assert.AreEqual(WebConstants.AdminArea, area.RouteValue);
        }

        [TestMethod]
        public void ThreadsController_ShouldAuthorizeAdmin()
        {
            //Arrange
            var authorization = typeof(ThreadsController)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AuthorizeAttribute)
                 as AuthorizeAttribute;

            //Assert
            Assert.IsNotNull(authorization);
            Assert.AreEqual(WebConstants.AdministratorRole, authorization.Roles);
        }
    }
}

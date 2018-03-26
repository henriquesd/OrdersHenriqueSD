using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OrdersHenriqueSD.Controllers;
using OrdersHenriqueSD.Models;
using OrdersHenriqueSD.Repositories;
using System.Net;

namespace OrdersHenriqueSDTests
{
    [TestFixture]
    public class TokenControllerTest
    {
        private TokenController _controller;
        private Mock<IUserPortalRepository> _userPortalRepositoryMock;
        private IConfiguration _config;

        [SetUp]
        public void Setup()
        {
            _userPortalRepositoryMock = new Mock<IUserPortalRepository>();
            _controller = new TokenController(_config, _userPortalRepositoryMock.Object);
        }

        [Test]
        public void Post_ShouldReturnNotFoundOnLogin_IfNameAndPasswordDontExist()
        {
            var expected = (int)HttpStatusCode.NotFound;

            var userPortal = new UserPortal
            {
                UserName = "test",
                Password = "test"
            };

            var result = ((ObjectResult)_controller.CreateToken(userPortal));

            Assert.AreEqual(expected, result.StatusCode);
        }

        [Test]
        public void Post_ShouldReturnErrorOnLogin_IfUserIsNull()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            var result = ((ObjectResult)_controller.CreateToken(null));

            Assert.AreEqual(expected, result.StatusCode);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Post_ShouldReturnErrorOnLogin_IfUserNameIsInvalid(string name)
        {
            var expected = (int)HttpStatusCode.BadRequest;
            var userPortal = new UserPortal
            {
                UserName = name,
                Password = "password"
            };

            var result = ((ObjectResult)_controller.CreateToken(userPortal));

            Assert.AreEqual(expected, result.StatusCode);
        }


        [TestCase(null)]
        [TestCase("")]
        public void Post_ShouldReturnErrorOnLogin_IfPasswordIsInvalid(string password)
        {
            var expected = (int)HttpStatusCode.BadRequest;
            var userPortal = new UserPortal
            {
                UserName = "name",
                Password = password
            };

            var result = ((ObjectResult)_controller.CreateToken(userPortal));

            Assert.AreEqual(expected, result.StatusCode);
        }
    }
}

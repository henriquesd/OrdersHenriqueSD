using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OrdersHenriqueSD.Controllers;
using OrdersHenriqueSD.Models;
using OrdersHenriqueSD.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OrdersHenriqueSDTests
{
    [TestFixture]
    public class UserPortalControllerTest
    {
        private UserPortalController _controller;
        private Mock<IUserPortalRepository> _userPortalRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userPortalRepositoryMock = new Mock<IUserPortalRepository>();
            _controller = new UserPortalController(_userPortalRepositoryMock.Object);
        }

        [Test]
        public void Get_ShouldListAllUsers()
        {
            var expected = new List<UserPortal>(){
              new UserPortal{
                  Id = 1,
                  Name = "Henrique",
                  UserName = "henriquesd",
                  Password = "test",
                  Email = "email@test.com",
                  CreationDate = DateTime.Now
              },
              new UserPortal {
                Id = 2,
                Name = "Neo",
                UserName = "neo",
                Password = "test",
                Email = "neo@test.com",
                CreationDate = DateTime.Now
              }
            };

            _userPortalRepositoryMock.Setup(m => m.GetUsers(null, null, null, DateTime.MinValue, DateTime.MinValue, null)).Returns(expected);

            var result = (List<UserPortal>)((ObjectResult)_controller.GetUsers(null, null, null, DateTime.MinValue, DateTime.MinValue, null)).Value;

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Post_ShouldReturnSuccessOnCreateAnUser_IfDataAreOk()
        {
            var user = GetUser();
            var statusCodeExpected = (int)HttpStatusCode.Created;
            _userPortalRepositoryMock.Setup(u => u.Create(It.IsAny<UserPortal>())).Returns(Task.FromResult(0));

            var actual = ((ObjectResult)_controller.Create(user).Result);

            Assert.AreEqual(statusCodeExpected, actual.StatusCode);
        }

        private static UserPortal GetUser(string name = "name", string userName = "userName", string password = "123456", string email = "email@email.com")
        {
            return new UserPortal
            {
                Name = name,
                UserName = userName,
                Password = password,
                Email = email
            };
        }

        [Test]
        public void Post_ShouldReturnErrorOnCreateAnUser_IfUserIsNull()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            _controller.ModelState.AddModelError("user", "The user is invalid.");

            var result = ((ObjectResult)_controller.Create(null).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("O")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Post_ShouldReturnErrorOnCreateAnUser_IfNameIsNotValid(string name)
        {
            var expected = (int)HttpStatusCode.BadRequest;

            UserPortal userPortal = new UserPortal
            {
                Name = name,
                UserName = "userName",
                Password = "password",
                Email = "email@email.com"
            };

            _controller.ModelState.AddModelError(nameof(name), "The name is invalid.");

            var result = ((ObjectResult)_controller.Create(userPortal).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("O")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Post_ShouldReturnErrorOnCreateAnUser_IfUserNameIsNotValid(string userName)
        {
            var expected = (int)HttpStatusCode.BadRequest;

            UserPortal userPortal = new UserPortal
            {
                Name = "Name",
                UserName = userName,
                Password = "password",
                Email = "email@email.com"
            };

            _controller.ModelState.AddModelError(nameof(userName), "The user name is invalid.");

            var result = ((ObjectResult)_controller.Create(userPortal).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("O")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Post_ShouldReturnErrorOnCreateAnUser_IfPasswordIsNotValid(string password)
        {
            var expected = (int)HttpStatusCode.BadRequest;

            UserPortal userPortal = new UserPortal
            {
                Name = "Name",
                UserName = "username",
                Password = password,
                Email = "email@email.com"
            };

            _controller.ModelState.AddModelError(nameof(password), "The password is invalid.");

            var result = ((ObjectResult)_controller.Create(userPortal).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }


        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("O")]
        [TestCase("email@")]
        [TestCase("@email")]
        [TestCase("email.com")]
        public void Post_ShouldReturnErrorOnCreateAnUser_IfEmailIsNotValid(string email)
        {
            var expected = (int)HttpStatusCode.BadRequest;

            UserPortal userPortal = new UserPortal
            {
                Name = "Name",
                UserName = "username",
                Password = "password",
                Email = email
            };

            _controller.ModelState.AddModelError(nameof(email), "The email is invalid.");

            var result = ((ObjectResult)_controller.Create(userPortal).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }
    }
}
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
    public class ProductControllerTest
    {
        private ProductController _controller;
        private Mock<IProductRepository> _productRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _controller = new ProductController(_productRepositoryMock.Object);
        }

        [Test]
        public void Get_ShoulListAllUsers()
        {
            var expected = new List<Product>(){
              new Product{
                  Id = 1,
                  Name = "Product 1",
                  Description = "Description 2",
                  Price = 10,
                  CreationDate = DateTime.Now
              },
              new Product {
                Id = 2,
                Name = "Product 2",
                Description = "Product 2",
                Price = 150,
                CreationDate = DateTime.Now
              }
            };

            _productRepositoryMock.Setup(m => m.GetProducts(null, null, 0, DateTime.MinValue, DateTime.MinValue, null)).Returns(expected);

            var result = (List<Product>)((ObjectResult)_controller.GetProducts(null, null, 0, DateTime.MinValue, DateTime.MinValue, null)).Value;

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Post_ShouldReturnSuccessOnCreateAnProduct_IfDataAreOk()
        {
            var user = GetProduct();
            var statusCodeExpected = (int)HttpStatusCode.Created;
            _productRepositoryMock.Setup(u => u.Create(It.IsAny<Product>())).Returns(Task.FromResult(0));

            var actual = ((ObjectResult)_controller.Create(user).Result);

            Assert.AreEqual(statusCodeExpected, actual.StatusCode);
        }

        private static Product GetProduct(string name = "Name", string description = "Description", decimal price = 10)
        {
            return new Product
            {
                Name = name,
                Description = description,
                Price = price
            };
        }


        [Test]
        public void Post_ShouldReturnErrorOnCreateAProduct_IfProductIsNull()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            var result = ((ObjectResult)_controller.Create(null).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("O")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Post_ShouldReturnErrorOnCreateAProduct_IfNameIsNotValid(string name)
        {
            var expected = (int)HttpStatusCode.BadRequest;

            Product product = new Product
            {
                Name = name,
                Description = "description",
                Price = 10
            };

            _controller.ModelState.AddModelError(nameof(product), "The product name is invalid.");

            var result = ((ObjectResult)_controller.Create(product).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("O")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Post_ShouldReturnErrorOnCreateAProduct_IfDescriptionIsNotValid(string description)
        {
            var expected = (int)HttpStatusCode.BadRequest;

            Product product = new Product
            {
                Name = "product",
                Description = description,
                Price = 10
            };

            _controller.ModelState.AddModelError(nameof(product), "The description is invalid.");

            var result = ((ObjectResult)_controller.Create(product).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Put_ShouldReturnSuccessOnEditAProduct_IfDataAreOk()
        {
            var expected = (int)HttpStatusCode.OK;

            int id = 1;
            Product product = new Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Price = 10
            };

            //var result = (ObjectResult)_productRepositoryMock.Setup(u => u.Edit(product));
            var result = ((ObjectResult)_controller.Edit(id, product).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Put_ShouldReturnErrorOnEditAProduct_IfIdIsNotValid()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            int id = 0;
            Product product = new Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Price = 10
            };

            var result = ((ObjectResult)_controller.Edit(id, product).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Put_ShouldReturnErrorOnEditAProduct_IfIdIsDifferentOfProductId()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            int id = 1;
            Product product = new Product
            {
                Id = 2,
                Name = "Product 1",
                Description = "Description 1",
                Price = 10
            };

            var result = ((ObjectResult)_controller.Edit(id, product).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }



    }
}

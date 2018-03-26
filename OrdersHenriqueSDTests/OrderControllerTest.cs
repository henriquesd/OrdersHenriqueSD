using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OrdersHenriqueSD.Controllers;
using OrdersHenriqueSD.Models;
using OrdersHenriqueSD.Repositories;
using System.Collections.Generic;
using System.Net;

namespace OrdersHenriqueSDTests
{
    [TestFixture]
    public class OrderControllerTest
    {
        private OrderController _controller;
        private Mock<IOrderRepository> _orderRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _controller = new OrderController(_orderRepositoryMock.Object);
        }

        [Test]
        public void Get_ShoulListAllOrders()
        {
            var expected = new List<Order>(){
              new Order{
                  Id = 1,
                  UserId = 1,
                  ProductOrderList = new List<ProductOrder>
                  {
                      new ProductOrder {
                      ProductId = 1,
                      Price = 10,
                      Quantity = 5
                      }
                  }
              },
            new Order{
                  Id = 2,
                  UserId = 1,
                    ProductOrderList = new List<ProductOrder>
                  {
                      new ProductOrder {
                      ProductId = 2,
                      Price = 15,
                      Quantity = 10
                  }
                  }
              }
            };

            _orderRepositoryMock.Setup(m => m.GetOrders()).Returns(expected);

            var result = (List<Order>)((ObjectResult)_controller.GetOrders()).Value;

            CollectionAssert.AreEqual(expected, result);

        }

        [Test]
        public void Post_ShouldReturnErrorOnCreateAnOrder_IfDataAreNotOk()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            var result = ((ObjectResult)_controller.Create(null).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Post_ShouldReturnErrorOnCreateAnOrder_IfUserIdIsNotValid()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            Order order = new Order
            {
                UserId = 0,
                ProductOrderList = new List<ProductOrder>
                  {
                      new ProductOrder {
                      ProductId = 1,
                      Price = 10,
                      Quantity = 5
                      }
                  }
            };

            var result = ((ObjectResult)_controller.Create(order).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Post_ShouldReturnErrorOnCreateAnOrder_IfProductListIsNull()
        {
            var expected = (int)HttpStatusCode.BadRequest;

            Order order = new Order
            {
                UserId = 1
            };

            var result = ((ObjectResult)_controller.Create(order).Result).StatusCode;

            Assert.AreEqual(expected, result);
        }

    }
}

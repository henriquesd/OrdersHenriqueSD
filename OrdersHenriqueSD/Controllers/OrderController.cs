using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersHenriqueSD.Models;
using OrdersHenriqueSD.Repositories;

namespace OrdersHenriqueSD.Controllers
{
    [Produces("application/json")]
    [Route("api"), Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult GetOrders()
        {
            return new ObjectResult(_orderRepository.GetOrders());
        }

        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform an order.");
                }

                if (order.UserId == 0)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform the User Id.");
                }

                if (order.ProductOrderList == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform the product.");
                }

                if (order.ProductOrderList.Count > 0)
                {
                    foreach (var item in order.ProductOrderList)
                    {
                        if (item.ProductId == 0)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, "Please inform the product.");
                        }
                        if (item.Price == 0)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, "Please inform the product price.");
                        }
                        if (item.Quantity == 0)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, "Please inform the product quantity.");
                        }
                    }
                }

               await _orderRepository.Create(order);

                return StatusCode((int)HttpStatusCode.OK, "Success!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
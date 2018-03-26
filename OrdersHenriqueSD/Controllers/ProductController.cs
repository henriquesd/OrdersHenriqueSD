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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts([FromQuery]
                string name = null,
                string description = null,
                decimal? price = null,
                DateTime? creationDateInitial = null,
                DateTime? creationDateFinal = null,
                string sortBy = null)
        {

            if (((creationDateInitial != null && creationDateFinal == null) ||
                    (creationDateInitial == null && creationDateFinal != null)) ||
               (creationDateInitial != DateTime.MinValue && creationDateFinal == DateTime.MinValue) ||
                (creationDateInitial == DateTime.MinValue && creationDateFinal != DateTime.MinValue))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "The filter by Creation Date you need to inform the Initial and the Final date.");
            }

            var products = _productRepository.GetProducts(name, description, price, creationDateInitial, creationDateFinal, sortBy);

            if (products == null)
            {
                return NotFound();
            }
            return new ObjectResult(products);
        }


        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid ||
                    product == null ||
                    product.Name == "" ||
                    product.Name == null ||
                    product.Description == "" ||
                    product.Description == null ||
                    product.Price == 0)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform all fields of product.");
                }

                await _productRepository.Create(product);

                return StatusCode((int)HttpStatusCode.Created, "Success!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Route("product")]
        public async Task<IActionResult> Edit(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null ||
                    product.Name == null || product.Name == "" ||
                    product.Description == null || product.Description == "" ||
                    product.Price == 0)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform all fields of product.");
                }

                if (product.Id != id)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform the same id of the product id.");
                }

               await _productRepository.Edit(product);

                return StatusCode((int)HttpStatusCode.OK, "Success!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
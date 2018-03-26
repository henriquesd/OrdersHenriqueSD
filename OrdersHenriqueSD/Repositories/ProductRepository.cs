using Microsoft.EntityFrameworkCore;
using OrdersHenriqueSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersHenriqueSD.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersHenriqueSDContext _context;

        public ProductRepository(OrdersHenriqueSDContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts(
            string name = null,
            string description = null,
            decimal? price = null,
            DateTime? creationDateInitial = null,
            DateTime? creationDateFinal = null,
            string sortBy = null)
        {
            var products = from p in _context.Products
                           select p;

            if (name != null && name != "")
                products = products.Where(p => p.Name.Contains(name));
            if (description != null && description != "")
                products = products.Where(p => p.Description.Contains(description));
            if ((creationDateInitial != null && creationDateFinal != null) &&
                (creationDateInitial != DateTime.MinValue && creationDateFinal != DateTime.MinValue))
                products = products.Where(p => p.CreationDate.Date >= creationDateInitial && p.CreationDate.Date <= creationDateFinal);

            if (sortBy != null && sortBy != "")
            {
                bool desc = false;
                if (sortBy.Substring(0, 1) == "-")
                {
                    sortBy = sortBy.Substring(1, sortBy.Length - 1);
                    desc = true;
                }

                switch (sortBy)
                {
                    case "name":
                        if (desc)
                            products = products.OrderByDescending(p => p.Name);
                        else
                            products = products.OrderBy(p => p.Name);
                        break;
                    case "description":
                        if (desc)
                            products = products.OrderByDescending(p => p.Description);
                        else
                            products = products.OrderBy(p => p.Description);
                        break;
                    case "price":
                        if (desc)
                            products = products.OrderByDescending(p => p.Price);
                        else
                            products = products.OrderBy(p => p.Price);
                        break;
                }
            }
            return products.ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(u => u.Id == id);

            return product;
        }

        public async Task Create(Product product)
        {
            product.CreationDate = DateTime.Now;
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}

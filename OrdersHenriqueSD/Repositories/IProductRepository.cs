using OrdersHenriqueSD.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersHenriqueSD.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts(
            string name = null,
            string description = null,
            decimal? price = null,
            DateTime? creationDateInitial = null,
            DateTime? creationDateFinal = null,
            string sortBy = null);

        Task<Product> GetProductById(int id);

        Task Create(Product product);

        Task Edit(Product product);
    }
}

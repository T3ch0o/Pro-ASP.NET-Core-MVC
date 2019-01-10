namespace SportsStore.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsStore.Models;
    using SportsStore.Services.Interfaces;

    public class ProductService : IProductService
    {
        public IQueryable<Product> GetAll => new List<Product>
        {
            new Product { Name = "Football", Price = 25 },
            new Product { Name = "Surf board", Price = 179 },
            new Product { Name = "Running shoes", Price = 95 }
        }.AsQueryable();
    }
}

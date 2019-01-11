namespace SportsStore.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsStore.Data;
    using SportsStore.Models;
    using SportsStore.Services.Interfaces;

    public class ProductService : IProductService
    {
        private readonly SportsStoreDbContext _db;

        public ProductService(SportsStoreDbContext db)
        {
            _db = db;
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}

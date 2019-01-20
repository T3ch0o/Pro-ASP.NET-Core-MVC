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

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                _db.Products.Add(product);
            }
            else
            {
                Product currentProduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);

                if (currentProduct != null)
                {
                    currentProduct.Name = product.Name;
                    currentProduct.Description = product.Description;
                    currentProduct.Price = product.Price;
                    currentProduct.Category = product.Category;
                }
            }

            _db.SaveChanges();
        }
    }
}

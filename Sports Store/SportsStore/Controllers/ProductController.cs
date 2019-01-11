namespace SportsStore.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;
    using SportsStore.Services.Interfaces;

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ViewResult List(int page = 1)
        {
            const int pageSize = 4;

            IEnumerable<Product> products = _productService.GetAll();

            ProductsListViewModel productsListModel = new ProductsListViewModel
            {
                Products = products
                           .OrderBy(p => p.Id)
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = products.Count()
                }
            };

            return View(productsListModel);
        }
    }
}

namespace SportsStore.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
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

            IEnumerable<Product> products = _productService.GetAll()
                                                .OrderBy(p => p.Id)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize);

            return View(products);
        }
    }
}

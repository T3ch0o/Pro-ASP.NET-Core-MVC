namespace SportsStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Services.Interfaces;

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ViewResult List()
        {
            return View(_productService.GetAll);
        }
    }
}

namespace SportsStore.Controllers
{
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Services.Interfaces;

    public class AdminController : Controller
    {
        private readonly IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        public ViewResult Index()
        {
            return View(_productService.GetAll());
        }

        public IActionResult Edit(int productId)
        {
            Product product = _productService.GetAll().FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}

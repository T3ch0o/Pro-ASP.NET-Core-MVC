namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;
    using SportsStore.Services.Interfaces;

    public class CartController : Controller
    {
        private readonly IProductService _productService;

        private readonly Cart _cart;

        public CartController(IProductService productService, Cart cart)
        {
            _productService = productService;
            _cart = cart;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl,
                HasItems = _cart.CartLines.Any()
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _productService.GetAll().FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                _cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _productService.GetAll().FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { ReturnUrl = returnUrl });
        }
    }
}
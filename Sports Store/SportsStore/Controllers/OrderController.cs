namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;
    using SportsStore.Services.Interfaces;

    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        private Cart _cart;

        public OrderController(IOrderService orderService, Cart cart)
        {
            _orderService = orderService;
            _cart = cart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!_cart.CartLines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.CartLines = _cart.CartLines.ToArray();
                _orderService.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }

            return View(order);
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
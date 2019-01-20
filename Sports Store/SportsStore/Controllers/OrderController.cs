namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
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

        public ViewResult List()
        {
            return View(_orderService.GetAll().Where(o => !o.Shipped));
        }

        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _orderService.GetAll().FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                order.Shipped = true;
                _orderService.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
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
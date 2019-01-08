namespace LanguageFeatures.Controllers
{
    using System.Collections.Generic;

    using LanguageFeatures.Models;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            List<string> results = new List<string>();

            foreach (Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";
                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }

            return View(results);
        }

        public ViewResult Products()
        {
            Product[] productArray =
            {
                    new Product
                    {
                        Name = "Kayak",
                        Price = 275M
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer ball",
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner flag",
                        Price = 34.95M
                    }
            };

            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20)
                                                   .TotalPrices();
            decimal nameFilterTotal = productArray.Filter(p => p?.Name?[0] == 'S')
                                                  .TotalPrices();

            return View("Index", new [] { $"Price Total: {priceFilterTotal:C2}", $"Name Total: {nameFilterTotal:C2}" });
        }
    }
}
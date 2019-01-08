namespace LanguageFeatures.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
                results.Add($"{nameof(p.Name)}: {name}, {nameof(p.Price)}: {price}, {nameof(p.Related)}: {relatedName}");
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

        public async Task<ViewResult> ContentLength()
        {
            long? length = await MyAsyncMethods.GetPathLengthAsync();

            return View("Index", new [] { $"Length: {length}" });
        }
    }
}
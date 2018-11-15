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
    }
}
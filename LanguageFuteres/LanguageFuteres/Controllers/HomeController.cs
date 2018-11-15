namespace LanguageFeatures.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View(new [] { "C#", "Language", "Features" });
        }
    }
}
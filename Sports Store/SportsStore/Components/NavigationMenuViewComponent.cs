namespace SportsStore.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Services.Interfaces;

    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public NavigationMenuViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            IEnumerable<string> productsCategory = _productService.GetAll()
                                                                    .Select(p => p.Category)
                                                                    .Distinct()
                                                                    .OrderBy(p => p);

            return View(productsCategory);
        }
    }
}

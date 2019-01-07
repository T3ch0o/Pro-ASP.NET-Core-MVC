namespace LanguageFeatures.Models
{
    using System.Collections.Generic;

    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> produts)
        {
            decimal total = 0;

            foreach (Product product in produts)
            {
                total += product?.Price ?? 0;
            }

            return total;
        }

        public static IEnumerable<Product> FilterByPrice(this IEnumerable<Product> productEnum, decimal minimumPrice)
        {
            foreach (Product product in productEnum)
            {
                if ((product?.Price ?? 0) >= minimumPrice)
                {
                    yield return product;
                }
            }
        }
    }
}

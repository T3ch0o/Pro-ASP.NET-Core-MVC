namespace LanguageFeatures.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public IEnumerable<Product> Products { get; set; }
    }
}

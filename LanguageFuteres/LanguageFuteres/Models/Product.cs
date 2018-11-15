namespace LanguageFeatures.Models
{
    public class Product
    {
        public string Name { get; set; }

        public Product Related { get; set; }

        public decimal? Price { get; set; }

        public static Product[] GetProducts()
        {
            Product kayak = new Product { Name = "Kayak", Price = 275M };
            Product lifejacket = new Product { Name = "Lifejacket", Price = 48.95M };

            kayak.Related = lifejacket;
            return new [] { kayak, lifejacket, null };
        }
    }
}

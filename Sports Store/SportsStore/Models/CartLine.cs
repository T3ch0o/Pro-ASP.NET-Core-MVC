namespace SportsStore.Models
{
    public class CartLine : BaseModel<int>
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

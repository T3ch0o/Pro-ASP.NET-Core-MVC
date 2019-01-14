namespace SportsStore.Tests.Models
{
    using System.Linq;

    using SportsStore.Models;

    using Xunit;

    public class CartTests
    {
        [Fact]
        public void CanAddNewLines()
        {
            // Arrange - create some test products
            Product product1 = new Product { Id = 1, Name = "P1" };
            Product product2 = new Product { Id = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            CartLine[] results = cart.CartLines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(product1, results[0].Product);
            Assert.Equal(product2, results[1].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            // Arrange - create some test products
            Product product1 = new Product { Id = 1, Name = "P1" };
            Product product2 = new Product { Id = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 10);

            CartLine[] results = cart.CartLines.OrderBy(c => c.Product.Id).ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void CanRemoveCartLines()
        {
            // Arrange - create some test products
            Product product1 = new Product { Id = 1, Name = "P1" };
            Product product2 = new Product { Id = 2, Name = "P2" };
            Product product3 = new Product { Id = 3, Name = "P3" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Arrange - add some products to the cart
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 5);
            cart.AddItem(product2, 1);

            // Act
            cart.RemoveLine(product2);

            // Assert
            Assert.Equal(0, cart.CartLines.Count(c => c.Product == product2));
            Assert.Equal(2, cart.CartLines.Count());
        }

        [Fact]
        public void CalculateCartTotal()
        {
            // Arrange - create some test products
            Product product1 = new Product { Id = 1, Name = "P1", Price = 100M };
            Product product2 = new Product { Id = 2, Name = "P2", Price = 50M };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 3);

            decimal result = cart.TotalCost;

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void CanClearContents()
        {
            // Arrange - create some test products
            Product product1 = new Product { Id = 1, Name = "P1", Price = 100M };
            Product product2 = new Product { Id = 2, Name = "P2", Price = 50M };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Arrange - add some items
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            // Act - reset the cart
            cart.Clear();

            // Assert
            Assert.Empty(cart.CartLines);
        }
    }
}
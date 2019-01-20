namespace SportsStore.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using SportsStore.Controllers;
    using SportsStore.Models;
    using SportsStore.Services.Interfaces;

    using Xunit;

    public class AdminControllerTests
    {
        [Fact]
        public void IndexContainsAllProducts()
        {
            // Arrange
            Mock<IProductService> mock = new Mock<IProductService>();

            mock.Setup(p => p.GetAll()).Returns(new[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            // Action
            Product[] result = GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void CanEditProduct()
        {
            // Arrange - create the mock repository
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
            }.AsQueryable());

            // Arrange - create the controller
            AdminController controller = new AdminController(mock.Object);

            // Act
            Product p1 = GetViewModel<Product>(controller.Edit(1));
            Product p2 = GetViewModel<Product>(controller.Edit(2));
            Product p3 = GetViewModel<Product>(controller.Edit(3));

            // Assert
            Assert.Equal(1, p1.Id);
            Assert.Equal(2, p2.Id);
            Assert.Equal(3, p3.Id);
        }

        [Fact]
        public void CannotEditNonexistentProduct()
        {
            // Arrange - create the mock repository
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns(new[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
            }.AsQueryable());

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act
            Product result = GetViewModel<Product>(target.Edit(4));

            // Assert
            Assert.Null(result);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
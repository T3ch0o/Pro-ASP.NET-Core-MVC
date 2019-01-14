namespace SportsStore.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ViewComponents;

    using Moq;

    using SportsStore.Components;
    using SportsStore.Controllers;
    using SportsStore.Models;
    using SportsStore.Models.ViewModels;
    using SportsStore.Services.Interfaces;

    using Xunit;

    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            // Arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns((new[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
                new Product { Id = 4, Name = "P4" },
                new Product { Id = 5, Name = "P5" }
            }).AsQueryable());
            ProductController controller = new ProductController(mock.Object);

            // Act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 1);
            Assert.Equal("P5", prodArray[0].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            // Arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns((new[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
                new Product { Id = 4, Name = "P4" },
                new Product { Id = 5, Name = "P5" }
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object);

            // Act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(4, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void CanFilterProducts()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns((new[]
            {
                new Product { Id = 1, Name = "P1", Category = "Cat1" },
                new Product { Id = 2, Name = "P2", Category = "Cat2" },
                new Product { Id = 3, Name = "P3", Category = "Cat1" },
                new Product { Id = 4, Name = "P4", Category = "Cat2" },
                new Product { Id = 5, Name = "P5", Category = "Cat3" }
            }).AsQueryable());

            // Arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);

            // Action
            Product[] result = (controller.List("Cat2").ViewData.Model as ProductsListViewModel)?.Products.ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void CanSelectCategories()
        {
            // Arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns((new[]
            {
                new Product { Id = 1, Name = "P1", Category = "Apples" },
                new Product { Id = 2, Name = "P2", Category = "Apples" },
                new Product { Id = 3, Name = "P3", Category = "Plums" },
                new Product { Id = 4, Name = "P4", Category = "Oranges" },
            }).AsQueryable());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)((ViewViewComponentResult)target.Invoke()).ViewData.Model).ToArray();

            // Assert
            Assert.True(new[] { "Apples", "Oranges", "Plums" }.SequenceEqual(results));
        }
    }
}
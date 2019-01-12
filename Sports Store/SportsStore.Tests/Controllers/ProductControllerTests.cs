namespace SportsStore.Tests.Controllers
{
    using System.Linq;

    using Moq;

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
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

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
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(4, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}
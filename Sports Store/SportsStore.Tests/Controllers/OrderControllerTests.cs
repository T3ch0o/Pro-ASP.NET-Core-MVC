﻿namespace SportsStore.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using SportsStore.Controllers;
    using SportsStore.Models;
    using SportsStore.Services.Interfaces;

    using Xunit;

    public class OrderControllerTests
    {
        [Fact]
        public void CannotCheckoutEmptyCart()
        {
            // Arrange - create a mock repository
            Mock<IOrderService> mock = new Mock<IOrderService>();

            // Arrange - create an empty cart
            Cart cart = new Cart();

            // Arrange - create the order
            Order order = new Order();

            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = target.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored
            mock.Verify(m => m.Save(It.IsAny<Order>()), Times.Never);

            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CannotCheckoutInvalidShippingDetails()
        {
            // Arrange - create a mock order repository
            Mock<IOrderService> mock = new Mock<IOrderService>();

            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // Assert - check that the order hasn't been passed stored
            mock.Verify(m => m.Save(It.IsAny<Order>()), Times.Never);

            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCheckoutAndSubmitOrder()
        {
            // Arrange - create a mock order repository
            Mock<IOrderService> mock = new Mock<IOrderService>();

            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Act - try to checkout
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            // Assert - check that the order has been stored
            mock.Verify(m => m.Save(It.IsAny<Order>()), Times.Once);

            // Assert - check that the method is redirecting to the Completed action
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
namespace SportsStore.Tests.TagHelpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    using Moq;

    using SportsStore.Models.ViewModels;
    using SportsStore.TagHelpers;

    using Xunit;

    public class PageLinkTagHelperTests
    {
        [Fact]
        public void CanGeneratePageLinks()
        {
            // Arrange
            Mock<IUrlHelper> urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock
                    .SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                    .Returns("Test/Page1")
                    .Returns("Test/Page2")
                    .Returns("Test/Page3");

            Mock<IUrlHelperFactory> urlHelperFactoryMock = new Mock<IUrlHelperFactory>();
            urlHelperFactoryMock
                    .Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                    .Returns(urlHelperMock.Object);

            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactoryMock.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            TagHelperContext ctx = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");
            Mock<TagHelperContent> contentMock = new Mock<TagHelperContent>();

            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(contentMock.Object));

            // Act
            helper.Process(ctx, output);

            // Assert
            Assert.Equal(@"<a href=""Test/Page1"">1</a><a href=""Test/Page2"">2</a><a href=""Test/Page3"">3</a>", output.Content.GetContent());
        }
    }
}
﻿namespace SportsStore.TagHelpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    using SportsStore.Models.ViewModels;

    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder divTag = new TagBuilder("div");

            for (int page = 1; page <= PageModel.TotalPages; page++)
            {
                TagBuilder anchorTag = new TagBuilder("a");
                anchorTag.Attributes["href"] = urlHelper.Action(PageAction, new { page });
                anchorTag.InnerHtml.Append(page.ToString());

                divTag.InnerHtml.AppendHtml(anchorTag);
            }

            output.Content.AppendHtml(divTag.InnerHtml);
        }
    }
}

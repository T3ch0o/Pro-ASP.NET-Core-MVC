namespace SportsStore.TagHelpers
{
    using System.Collections.Generic;

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

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder divTag = new TagBuilder("div");

            for (int page = 1; page <= PageModel.TotalPages; page++)
            {
                TagBuilder anchorTag = new TagBuilder("a");
                PageUrlValues["page"] = page;
                anchorTag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                if (PageClassesEnabled)
                {
                    anchorTag.AddCssClass(PageClass);
                    anchorTag.AddCssClass(page == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                anchorTag.InnerHtml.Append(page.ToString());

                divTag.InnerHtml.AppendHtml(anchorTag);
            }

            output.Content.AppendHtml(divTag.InnerHtml);
        }
    }
}

namespace SportsStore.TagHelpers
{
    using System.Web;

    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("a", Attributes = "page-controller")]
    public class AnchorLinkTagHelper : TagHelper
    {
        public string PageArea { get; set; }

        public string PageController { get; set; }

        public string PageAction { get; set; }

        public string PageReturnUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            if (!PageReturnUrl.Contains('%'))
            {
                PageReturnUrl = HttpUtility.UrlEncode(PageReturnUrl);
            }

            output.Attributes.Add("href", $"/{PageController}/{PageAction}?returnUrl={PageReturnUrl}");
        }
    }
}

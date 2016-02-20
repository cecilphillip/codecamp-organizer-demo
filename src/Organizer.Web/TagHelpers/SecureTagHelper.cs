using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Organizer.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = RoleAttributeName)]
    public class SecureTagHelper : TagHelper
    {
        private const string RoleAttributeName = "asp-secure-role";

        [HtmlAttributeName(RoleAttributeName)]
        public string Role { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext.HttpContext.User.Identity.IsAuthenticated && ViewContext.HttpContext.User.IsInRole(Role))
            {
                base.Process(context, output);
                return;
            }

            output.SuppressOutput();          
        }
    }
}

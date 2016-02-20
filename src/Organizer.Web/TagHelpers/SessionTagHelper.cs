using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.TagHelpers;
using Organizer.Web.ViewModels;

namespace Organizer.Web.TagHelpers
{
    [HtmlTargetElement("session")]
    public class SessionTagHelper : TagHelper
    {
        private const string ModelAttributeName = "model";

        private string Template = @"
 <div class=""col s12 m4"">
                <div class=""card hoverable smaller"">
                    <div class=""card-content"">
                        <i class=""material-icons right activator"">more_vert</i>
                        <span class=""card-title activator grey-text text-darken-4"">{0}</span>
                        <p><span class=""light-blue-text darken-2"">{1}</span></p>
                    </div>
                    <div class=""card-reveal"">                        
                        <span class=""card-title grey-text text-darken-4"">{0}</span>
                        <span class=""light-blue-text darken-2"">{1}</span>
                        <p><span class=""brown-text darken-1 "">{2}</span></p>
                        <p>Imagine if I had imported the descriptions.Thats what would go here!</p>
                    </div>
                </div>
            </div>
";
        [HtmlAttributeName(ModelAttributeName)]
        public SessionViewModel Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var markup = string.Format(Template, Model.Name, Model.Speaker.Name, Model.Track);
            
            output.Content.SetHtmlContent(markup);
           
        }
    }
}


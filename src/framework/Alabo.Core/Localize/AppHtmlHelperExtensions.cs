using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alabo.Core.Localize
{
    public static class AppHtmlHelperExtensions
    {
        public static IHtmlContent T(this IHtmlHelper html, string input)
        {
            return new LocalizedHtmlString(input);
        }
    }
}
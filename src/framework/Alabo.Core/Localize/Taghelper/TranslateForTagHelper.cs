﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Alabo.Extensions;

namespace Alabo.Core.Localize.Taghelper
{
    [HtmlTargetElement("a", Attributes = ForAttributeName)]
    [HtmlTargetElement("label", Attributes = ForAttributeName)]
    [HtmlTargetElement("textbox", Attributes = ForAttributeName)]
    [HtmlTargetElement("span", Attributes = ForAttributeName)]
    [HtmlTargetElement("div", Attributes = ForAttributeName)]
    [HtmlTargetElement("input", Attributes = ForAttributeName)]
    public class TranslateForTagHelper : TagHelper
    {
        private const string ForAttributeName = "translate-for";

        private const string TextAttributeName = "content";

        [HtmlAttributeName(ForAttributeName)] public string TranslateFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var translateForArray = TranslateFor.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            translateForArray.Foreach(async e =>
            {
                if (e.Equals(TextAttributeName, StringComparison.OrdinalIgnoreCase))
                {
                    var content = await output.GetChildContentAsync();
                    output.Content.SetHtmlContent(new LocalizedString(content.GetContent()).ToString());
                }
                else
                {
                    var find = output.Attributes.FirstOrDefault(a =>
                        a.Name.Equals(e, StringComparison.OrdinalIgnoreCase));
                    if (find != null) {
                        output.Attributes.SetAttribute(find.Name,
                            new LocalizedString(Convert.ToString(find.Value)).ToString());
                    }
                }
            });
        }
    }
}
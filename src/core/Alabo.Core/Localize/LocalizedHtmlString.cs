using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Alabo.Core.Localize
{
    public class LocalizedHtmlString : IHtmlContent
    {
        private readonly LocalizedString _localizedString;

        public LocalizedHtmlString(string input)
        {
            _localizedString = new LocalizedString(input);
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (writer == null) {
                throw new ArgumentNullException(nameof(writer));
            }

            if (encoder == null) {
                throw new ArgumentNullException(nameof(encoder));
            }

            writer.Write(_localizedString.ToString());
        }

        public override string ToString()
        {
            return _localizedString.ToString();
        }
    }
}
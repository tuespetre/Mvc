// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNet.Html.Abstractions;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    public struct RazorValue
    {
        private readonly bool _needsEncoding;
        private readonly object _value;

        public RazorValue(string encoded, bool needsEncoding)
        {
            _value = encoded;
            _needsEncoding = needsEncoding;
        }

        public RazorValue(IHtmlContent content)
        {
            _value = content;
            _needsEncoding = false;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_value == null)
            {
                return;
            }

            var stringValue = _value as string;
            if (stringValue != null)
            {
                if (_needsEncoding)
                {
                    encoder.Encode(writer, stringValue);
                }
                else
                {
                    writer.Write(stringValue);
                }

                return;
            }

            var htmlContentValue = _value as IHtmlContent;
            if (htmlContentValue != null)
            {
                htmlContentValue.WriteTo(writer, encoder);
                return;
            }

            Debug.Fail("We shouldn't get here.");
        }
    }
}

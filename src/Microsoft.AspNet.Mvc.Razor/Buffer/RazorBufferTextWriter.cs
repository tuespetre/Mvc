// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Internal;
using Microsoft.AspNet.Mvc.ViewFeatures;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    /// <summary>
    /// A <see cref="HtmlTextWriter"/> that stores individual write operations as a sequence of
    /// <see cref="string"/> and <see cref="IHtmlContent"/> instances.
    /// </summary>
    /// <remarks>
    /// This is primarily designed to avoid creating large in-memory strings.
    /// Refer to https://aspnetwebstack.codeplex.com/workitem/585 for more details.
    /// </remarks>
    public class RazorBufferTextWriter : HtmlTextWriter
    {
        private const int MaxCharToStringLength = 1024;

        private readonly Encoding _encoding;
        private readonly RazorBuffer _buffer;

        public RazorBufferTextWriter(RazorBuffer buffer, Encoding encoding)
        {
            _buffer = buffer;
            _encoding = encoding;
        }

        /// <inheritdoc />
        public override Encoding Encoding
        {
            get { return _encoding; }
        }

        /// <summary>
        /// Gets the content written to the writer as an <see cref="IHtmlContent"/>.
        /// </summary>
        public IHtmlContent Content => _buffer;

        /// <inheritdoc />
        public override void Write(char value)
        {
            _buffer.Append(value.ToString());
        }

        /// <inheritdoc />
        public override void Write(char[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (count < 0 || (buffer.Length - index < count))
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            while (count > 0)
            {
                // Split large char arrays into 1KB strings.
                var currentCount = count;
                if (MaxCharToStringLength < currentCount)
                {
                    currentCount = MaxCharToStringLength;
                }

                _buffer.Append(new string(buffer, index, currentCount));
                index += currentCount;
                count -= currentCount;
            }
        }

        /// <inheritdoc />
        public override void Write(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            _buffer.Append(value);
        }

        /// <inheritdoc />
        public override void Write(IHtmlContent value)
        {
            _buffer.Append(value);
        }

        /// <inheritdoc />
        public override Task WriteAsync(char value)
        {
            Write(value);
            return TaskCache.CompletedTask;
        }

        /// <inheritdoc />
        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            Write(buffer, index, count);
            return TaskCache.CompletedTask;
        }

        /// <inheritdoc />
        public override Task WriteAsync(string value)
        {
            Write(value);
            return TaskCache.CompletedTask;
        }

        /// <inheritdoc />
        public override void WriteLine()
        {
            _buffer.Append(Environment.NewLine);
        }

        /// <inheritdoc />
        public override void WriteLine(string value)
        {
            Write(value);
            WriteLine();
        }

        /// <inheritdoc />
        public override Task WriteLineAsync(char value)
        {
            WriteLine(value);
            return TaskCache.CompletedTask;
        }

        /// <inheritdoc />
        public override Task WriteLineAsync(char[] value, int start, int offset)
        {
            WriteLine(value, start, offset);
            return TaskCache.CompletedTask;
        }

        /// <inheritdoc />
        public override Task WriteLineAsync(string value)
        {
            WriteLine(value);
            return TaskCache.CompletedTask;
        }

        /// <inheritdoc />
        public override Task WriteLineAsync()
        {
            WriteLine();
            return TaskCache.CompletedTask;
        }

        /// <summary>
        /// If the specified <paramref name="writer"/> is a <see cref="HtmlTextWriter"/> the contents
        /// are copied. It is just written to the <paramref name="writer"/> otherwise.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to which the content must be copied/written.</param>
        /// <param name="encoder">The <see cref="HtmlEncoder"/> to encode the copied/written content.</param>
        public void CopyTo(TextWriter writer, HtmlEncoder encoder)
        {
            var htmlTextWriter = writer as HtmlTextWriter;
            if (htmlTextWriter != null)
            {
                htmlTextWriter.Write(Content);
            }
            else
            {
                Content.WriteTo(writer, encoder);
            }
        }

        /// <summary>
        /// If the specified <paramref name="writer"/> is a <see cref="HtmlTextWriter"/> the contents
        /// are copied. It is just written to the <paramref name="writer"/> otherwise.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to which the content must be copied/written.</param>
        /// <param name="encoder">The <see cref="HtmlEncoder"/> to encode the copied/written content.</param>
        public Task CopyToAsync(TextWriter writer, HtmlEncoder encoder)
        {
            CopyTo(writer, encoder);
            return TaskCache.CompletedTask;
        }
    }
}

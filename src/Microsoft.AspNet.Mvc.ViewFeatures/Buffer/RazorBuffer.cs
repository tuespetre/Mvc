// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNet.Html.Abstractions;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    [DebuggerDisplay("{DebuggerToString()}")]
    public class RazorBuffer : IHtmlContentBuilder
    {
        private readonly RazorBufferScope _scope;
        private readonly string _name;

        private List<RazorBufferChunk> _chunks;
        private int _currentIndex;

        public RazorBuffer(RazorBufferScope scope, string name)
        {
            if (scope == null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            _scope = scope;
            _name = name;
        }

        public IHtmlContentBuilder Append(string unencoded)
        {
            if (unencoded == null)
            {
                return this;
            }

            EnsureCapacity();

            var chunk = _chunks[_chunks.Count - 1];
            chunk.Data.Array[chunk.Data.Offset + _currentIndex] = new RazorValue(unencoded, needsEncoding: true);
            _currentIndex = (_currentIndex + 1) % chunk.Data.Count;
            return this;
        }

        public IHtmlContentBuilder Append(IHtmlContent content)
        {
            if (content == null)
            {
                return this;
            }

            EnsureCapacity();

            var chunk = _chunks[_chunks.Count - 1];
            chunk.Data.Array[chunk.Data.Offset + _currentIndex] = new RazorValue(content);
            _currentIndex = (_currentIndex + 1) % chunk.Data.Count;
            return this;
        }

        public IHtmlContentBuilder AppendHtml(string encoded)
        {
            if (encoded == null)
            {
                return this;
            }

            EnsureCapacity();

            var chunk = _chunks[_chunks.Count - 1];
            chunk.Data.Array[chunk.Data.Offset + _currentIndex] = new RazorValue(encoded, needsEncoding: false);
            _currentIndex = (_currentIndex + 1) % chunk.Data.Count;
            return this;
        }

        public IHtmlContentBuilder Clear()
        {
            throw new NotImplementedException();
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            for (var i = 0; i < _chunks.Count; i++)
            {
                var chunk = _chunks[i];
                var count = i + 1 < _chunks.Count ? chunk.Data.Count : _currentIndex;

                for (var j = 0; j < count; j++)
                {
                    var value = chunk.Data.Array[chunk.Data.Offset + j];
                    value.WriteTo(writer, encoder);
                }
            }
        }

        private RazorBufferChunk EnsureCapacity()
        {
            if (_chunks == null)
            {
                _chunks = new List<RazorBufferChunk>()
                {
                    _scope.GetChunk(),
                };
            };

            var chunk = _chunks[_chunks.Count - 1];
            if (_currentIndex == chunk.Data.Count - 1)
            {
                chunk = _scope.GetChunk();
                _chunks.Add(chunk);
                return chunk;
            }
            else
            {
                return chunk;
            }
        }

        private string DebuggerToString()
        {
            return _name;
        }
    }
}

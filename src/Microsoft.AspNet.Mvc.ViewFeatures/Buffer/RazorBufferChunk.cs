// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    public struct RazorBufferChunk
    {
        public static readonly int Size = 256;

        public RazorBufferChunk(ArraySegment<RazorValue> data)
        {
            Data = data;
        }

        public ArraySegment<RazorValue> Data { get; }
    }
}

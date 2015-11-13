// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    public class TestRazorBufferScope : RazorBufferScope
    {
        public override RazorBufferChunk GetChunk()
        {
            return new RazorBufferChunk(new ArraySegment<RazorValue>(new RazorValue[128]));
        }
    }
}

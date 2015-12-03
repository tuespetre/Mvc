// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Mvc.ViewFeatures.Buffer
{
    public class TestViewBufferScope : IViewBufferScope
    {
        public const int BufferSize = 128;
        private readonly int _offset;
        private readonly int _count;

        public TestViewBufferScope()
            : this(0, BufferSize)
        {

        }

        public TestViewBufferScope(int offset, int count)
        {
            _offset = offset;
            _count = count;
        }

        public ViewBufferSegment GetSegment()
        {
            var values = new ViewBufferValue[BufferSize];
            return new ViewBufferSegment(new ArraySegment<ViewBufferValue>(values, _offset, _count));
        }
    }
}

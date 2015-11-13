// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.MemoryPool;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    public class MemoryPoolRazorBufferScope : RazorBufferScope
    {
        private readonly IArraySegmentPool<RazorValue> _pool;
        private List<LeasedArraySegment<RazorValue>> _leased;

        public MemoryPoolRazorBufferScope(IArraySegmentPool<RazorValue> pool)
        {
            _pool = pool;

            _leased = new List<LeasedArraySegment<RazorValue>>();
        }

        public override RazorBufferChunk GetChunk()
        {
            LeasedArraySegment<RazorValue> segment = null;

            try
            {
                segment = _pool.Lease(1024);
                _leased.Add(segment);
                return new RazorBufferChunk(segment.Data);
            }
            catch
            {
                if (segment != null)
                {
                    segment.Owner.Return(segment);
                }

                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (var i = 0; i < _leased.Count; i++)
                {
                    var segment = _leased[i];
                    segment.Owner.Return(segment);
                }

                _leased.Clear();
            }
        }
    }
}

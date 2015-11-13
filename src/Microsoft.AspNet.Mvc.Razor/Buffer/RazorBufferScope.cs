// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Mvc.Razor.Buffer
{
    public abstract class RazorBufferScope : IDisposable
    {
        private bool _isDisposed;

        public virtual RazorBuffer CreateBuffer(string name)
        {
            return new RazorBuffer(this, name);
        }

        public abstract RazorBufferChunk GetChunk();

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            Dispose(disposing: true);

            _isDisposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Mvc.ViewFeatures.Buffer
{
    /// <summary>
    /// Creates and manages the lifetime of <see cref="ViewBufferSegment"/> instances.
    /// </summary>
    public interface IViewBufferScope
    {
        /// <summary>
        /// Gets a <see cref="ViewBufferSegment"/>.
        /// </summary>
        /// <returns>The <see cref="ViewBufferSegment"/>.</returns>
        ViewBufferSegment GetSegment();
    }
}

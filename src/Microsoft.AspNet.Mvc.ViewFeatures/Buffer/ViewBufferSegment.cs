// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Mvc.ViewFeatures.Buffer
{
    /// <summary>
    /// Encapsulates a <see cref="ArraySegment{ViewBufferValue}"/>.
    /// </summary>
    public struct ViewBufferSegment
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ViewBufferSegment"/>.
        /// </summary>
        /// <param name="data">The <see cref="ArraySegment{ViewBufferValue}"/> to encapsulate.</param>
        public ViewBufferSegment(ArraySegment<ViewBufferValue> data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the <see cref="ArraySegment{ViewBufferValue}"/>.
        /// </summary>
        public ArraySegment<ViewBufferValue> Data { get; }
    }
}

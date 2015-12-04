// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    /// <summary>
    /// A common base class for <see cref="ModelValidationContext"/> and <see cref="ClientModelValidationContext"/>.
    /// </summary>
    public class ModelValidationContextBase
    {
        public ModelValidationContextBase(
            ActionContext actionContext,
            ModelMetadata modelMetadata,
            IModelMetadataProvider metadataProvider)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            if (modelMetadata == null)
            {
                throw new ArgumentNullException(nameof(modelMetadata));
            }

            if (metadataProvider == null)
            {
                throw new ArgumentNullException(nameof(metadataProvider));
            }

            ActionContext = actionContext;
            ModelMetadata = modelMetadata;
            MetadataProvider = metadataProvider;
        }

        /// <summary>
        /// Gets or sets the <see cref="Mvc.ActionContext"/>
        /// </summary>
        public ActionContext ActionContext { get; }

        /// <summary>
        /// Gets or sets the <see cref="ModelBinding.ModelMetadata"/> associated with <see cref="Model"/>.
        /// </summary>
        public ModelMetadata ModelMetadata { get; }

        /// <summary>
        /// Gets or sets the <see cref="IModelMetadataProvider"/> 
        /// </summary>
        public IModelMetadataProvider MetadataProvider { get; }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    public class CompareAttributeAdapter : AttributeAdapterBase<CompareAttribute>
    {
        public CompareAttributeAdapter(CompareAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var errorMessage = GetErrorMessage(context);
            var clientRule = new ModelClientValidationEqualToRule(errorMessage, "*." + Attribute.OtherProperty);
            return new[] { clientRule };
        }

        /// <inheritdoc />
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            var displayName = validationContext.ModelMetadata.GetDisplayName();
            var otherPropertyDisplayName = GetOtherPropertyDisplayName(validationContext);
            return GetErrorMessage(validationContext.ModelMetadata, displayName, otherPropertyDisplayName);
        }

        private string GetOtherPropertyDisplayName(ModelValidationContextBase validationContext)
        {
            // The System.ComponentModel.DataAnnotations.CompareAttribute doesn't populate the
            // OtherPropertyDisplayName until after IsValid() is called. Therefore, at the time we get
            // the error message for client validation, the display name is not populated and won't be used.
            var otherPropertyDisplayName = Attribute.OtherPropertyDisplayName;
            if (otherPropertyDisplayName == null && validationContext.ModelMetadata.ContainerType != null)
            {
                var otherProperty = validationContext.MetadataProvider.GetMetadataForProperty(
                    validationContext.ModelMetadata.ContainerType,
                    Attribute.OtherProperty);
                if (otherProperty != null)
                {
                    return otherProperty.GetDisplayName();
                }
            }

            return Attribute.OtherProperty;
        }
    }
}
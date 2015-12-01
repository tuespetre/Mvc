// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNet.Mvc.DataAnnotations;

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

            var errorMessage = GetErrorMessage(context.ModelMetadata, context.MetadataProvider);
            var clientRule = new ModelClientValidationEqualToRule(errorMessage,
                                                            FormatPropertyForClientValidation(Attribute.OtherProperty));
            return new[] { clientRule };
        }

        public override string GetErrorMessage(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
        {
            var displayName = metadata.GetDisplayName();

            var otherPropertyDisplayName = GetOtherPropertyDisplayName(metadata, metadataProvider);

            return GetErrorMessage(metadata, displayName, otherPropertyDisplayName);
        }

        private string GetOtherPropertyDisplayName(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
        {
            // The System.ComponentModel.DataAnnotations.CompareAttribute doesn't populate the
            // OtherPropertyDisplayName until after IsValid() is called. Therefore, by the time we get
            // the error message for client validation, the display name is not populated and won't be used.
            var otherPropertyDisplayName = Attribute.OtherPropertyDisplayName;
            if (otherPropertyDisplayName == null && metadata.ContainerType != null)
            {
                var otherProperty = metadataProvider.GetMetadataForProperty(
                    metadata.ContainerType,
                    Attribute.OtherProperty);
                if (otherProperty != null)
                {
                    return otherProperty.GetDisplayName();
                }
            }

            return Attribute.OtherProperty;
        }

        private static string FormatPropertyForClientValidation(string property)
        {
            return "*." + property;
        }
    }
}
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    public class CompareAttributeAdapter : DataAnnotationsClientModelValidator<CompareAttribute>
    {
        public CompareAttributeAdapter(CompareAttribute attribute, IStringLocalizer stringLocalizer)
            : base(new CompareAttributeWrapper(attribute), stringLocalizer)
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

        public string GetErrorMessage(ModelMetadata metadata, IModelMetadataProvider metdataProvider)
        {
            return ((CompareAttributeWrapper)Attribute).FormatErrorMessage(metadata, metdataProvider);
        }

        private static string FormatPropertyForClientValidation(string property)
        {
            return "*." + property;
        }

        private sealed class CompareAttributeWrapper : CompareAttribute
        {
            public CompareAttributeWrapper(CompareAttribute attribute)
                : base(attribute.OtherProperty)
            {
                // Copy settable properties from wrapped attribute. Don't reset default message accessor (set as
                // CompareAttribute constructor calls ValidationAttribute constructor) when all properties are null to
                // preserve default error message. Reset the message accessor when just ErrorMessageResourceType is
                // non-null to ensure correct InvalidOperationException.
                if (!string.IsNullOrEmpty(attribute.ErrorMessage) ||
                    !string.IsNullOrEmpty(attribute.ErrorMessageResourceName) ||
                    attribute.ErrorMessageResourceType != null)
                {
                    ErrorMessage = attribute.ErrorMessage;
                    ErrorMessageResourceName = attribute.ErrorMessageResourceName;
                    ErrorMessageResourceType = attribute.ErrorMessageResourceType;
                }
            }

            public string FormatErrorMessage(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
            {
                
                var displayName = metadata.GetDisplayName();
                return string.Format(CultureInfo.CurrentCulture,
                                     ErrorMessageString,
                                     displayName,
                                     GetOtherPropertyDisplayName(metadata, metadataProvider));
            }

            private string GetOtherPropertyDisplayName(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
            {
                // The System.ComponentModel.DataAnnotations.CompareAttribute doesn't populate the
                // OtherPropertyDisplayName until after IsValid() is called. Therefore, by the time we get
                // the error message for client validation, the display name is not populated and won't be used.
                var otherPropertyDisplayName = OtherPropertyDisplayName;
                if (otherPropertyDisplayName == null && metadata.ContainerType != null)
                {
                    var otherProperty = metadataProvider.GetMetadataForProperty(
                        metadata.ContainerType,
                        OtherProperty);
                    if (otherProperty != null)
                    {
                        return otherProperty.GetDisplayName();
                    }
                }

                return OtherProperty;
            }
        }
    }
}
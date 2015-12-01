// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    public class StringLengthAttributeAdapter : AttributeAdapterBase<StringLengthAttribute>
    {
        public StringLengthAttributeAdapter(StringLengthAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var errorMessage = GetErrorMessage(context.ModelMetadata);
            var rule = new ModelClientValidationStringLengthRule(errorMessage,
                                                                 Attribute.MinimumLength,
                                                                 Attribute.MaximumLength);
            return new[] { rule };
        }

        public override string GetErrorMessage(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
        {
            return GetErrorMessage(
                metadata,
                metadata.GetDisplayName(),
                Attribute.MinimumLength,
                Attribute.MaximumLength);
        }
    }
}

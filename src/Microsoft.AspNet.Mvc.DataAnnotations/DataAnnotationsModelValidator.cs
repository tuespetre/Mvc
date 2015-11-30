// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{

    public class DataAnnotationsModelValidator : IModelValidator
    {
        private IStringLocalizer _stringLocalizer;

        public DataAnnotationsModelValidator(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            Attribute = attribute;
            _stringLocalizer = stringLocalizer;
        }

        public ValidationAttribute Attribute { get; }

        private IDictionary<Type, Func<ModelMetadata, string>> BuildErrorMessageDict(
            IModelMetadataProvider metadataProvider)
        {
            return new Dictionary<Type, Func<ModelMetadata, string>>
            {
                {
                    typeof(RangeAttribute), (modelMetadata) =>
                    {
                        return new RangeAttributeAdapter((RangeAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(RequiredAttribute), (modelMetadata) =>
                    {
                        return new RequiredAttributeAdapter((RequiredAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(MaxLengthAttribute), (modelMetadata) =>
                    {
                        return new MaxLengthAttributeAdapter((MaxLengthAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(MinLengthAttribute), (modelMetadata) =>
                    {
                        return new MinLengthAttributeAdapter((MinLengthAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(RegularExpressionAttribute), (modelMetadata) =>
                    {
                        return new RegularExpressionAttributeAdapter((RegularExpressionAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(CompareAttribute), (modelMetadata) =>
                    {
                        return new CompareAttributeAdapter((CompareAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata, metadataProvider);

                    }
                },
                {
                    typeof(StringLengthAttribute), (modelMetadata) =>
                    {
                        return new StringLengthAttributeAdapter((StringLengthAttribute)Attribute, _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(CreditCardAttribute), (modelMetadata) =>
                    {
                        return new DataTypeAttributeAdapter((DataTypeAttribute)Attribute, "creditcard", _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(EmailAddressAttribute), (modelMetadata) =>
                    {
                        return new DataTypeAttributeAdapter((DataTypeAttribute)Attribute, "email", _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(PhoneAttribute), (modelMetadata) =>
                    {
                        return new DataTypeAttributeAdapter((DataTypeAttribute)Attribute, "phone", _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                },
                {
                    typeof(UrlAttribute), (modelMetadata) =>
                    {
                        return new DataTypeAttributeAdapter((DataTypeAttribute)Attribute, "url", _stringLocalizer)
                            .GetErrorMessage(modelMetadata);
                    }
                }
            };
        }

        private string GetErrorMessage(ModelMetadata metadata, IModelMetadataProvider metadataProvider)
        {
            if (Attribute == null)
            {
                throw new ArgumentNullException(nameof(Attribute));
            }


            var attrType = Attribute.GetType();

            var dict = BuildErrorMessageDict(metadataProvider);

            if (dict.ContainsKey(attrType))
            {
                var func = dict[attrType];
                return func(metadata);
            }
            else
            {
                throw new NotImplementedException($"Error message localizer not defined for {attrType.Name}");
            }
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext validationContext)
        {
            var metadata = validationContext.Metadata;
            var memberName = metadata.PropertyName ?? metadata.ModelType.Name;
            var container = validationContext.Container;

            var context = new ValidationContext(container ?? validationContext.Model)
            {
                DisplayName = metadata.GetDisplayName(),
                MemberName = memberName
            };

            var metadataProvider = (IModelMetadataProvider)context.GetService(typeof(IModelMetadataProvider));

            var result = Attribute.GetValidationResult(validationContext.Model, context);
            if (result != ValidationResult.Success)
            {
                // ModelValidationResult.MemberName is used by invoking validators (such as ModelValidator) to
                // construct the ModelKey for ModelStateDictionary. When validating at type level we want to append
                // the returned MemberNames if specified (e.g. person.Address.FirstName). For property validation, the
                // ModelKey can be constructed using the ModelMetadata and we should ignore MemberName (we don't want
                // (person.Name.Name). However the invoking validator does not have a way to distinguish between these
                // two cases. Consequently we'll only set MemberName if this validation returns a MemberName that is
                // different from the property being validated.

                var errorMemberName = result.MemberNames.FirstOrDefault();
                if (string.Equals(errorMemberName, memberName, StringComparison.Ordinal))
                {
                    errorMemberName = null;
                }

                string errorMessage = null;
                if (_stringLocalizer != null &&
                    !string.IsNullOrEmpty(Attribute.ErrorMessage) &&
                    string.IsNullOrEmpty(Attribute.ErrorMessageResourceName) &&
                    Attribute.ErrorMessageResourceType == null)
                {

                    errorMessage = GetErrorMessage(metadata, metadataProvider);
                }

                var validationResult = new ModelValidationResult(errorMemberName, errorMessage ?? result.ErrorMessage);
                return new ModelValidationResult[] { validationResult };
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}

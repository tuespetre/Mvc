// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.DataAnnotations
{
    /// <summary>
    /// Creates a <see cref="IAttributeAdapter"/> for the given attribute.
    /// </summary>
    public class ValidationAttributeAdapterProvider
    {
        /// <summary>
        /// Creates a <see cref="IAttributeAdapter"/> for the given attribute.
        /// </summary>
        /// <param name="attribute">The attribute to create an adapter for.</param>
        /// <param name="stringLocalizer">The localizer to provide to the adapter.</param>
        /// <returns>An <see cref="IAttributeAdapter"/> for the given attribute.</returns>
        public static IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            IAttributeAdapter adapter;

            var type = attribute.GetType();

            if (type == typeof(RegularExpressionAttribute))
            {
                adapter = GetRegularExpressionAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(MaxLengthAttribute))
            {
                adapter = GetMaxLengthAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(RequiredAttribute))
            {
                adapter = GetRequiredAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(CompareAttribute))
            {
                adapter = GetCompareAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(MinLengthAttribute))
            {
                adapter = GetMinLengthAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(CreditCardAttribute))
            {
                adapter = GetCreditCardAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(StringLengthAttribute))
            {
                adapter = GetStringLengthAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(RangeAttribute))
            {
                adapter = GetRangeAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(EmailAddressAttribute))
            {
                adapter = GetEmailAddressAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(PhoneAttribute))
            {
                adapter = GetPhoneAdapter(attribute, stringLocalizer);
            }
            else if (type == typeof(UrlAttribute))
            {
                adapter = GetUrlAdapter(attribute, stringLocalizer);
            }
            else
            {
                adapter = null;
            }

            return adapter;
        }

        private static RegularExpressionAttributeAdapter GetRegularExpressionAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new RegularExpressionAttributeAdapter(
                        (RegularExpressionAttribute)attribute,
                        stringLocalizer);

        private static RequiredAttributeAdapter GetRequiredAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new RequiredAttributeAdapter(
                        (RequiredAttribute)attribute,
                        stringLocalizer);

        private static CompareAttributeAdapter GetCompareAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new CompareAttributeAdapter(
                        (CompareAttribute)attribute,
                        stringLocalizer);

        private static MinLengthAttributeAdapter GetMinLengthAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new MinLengthAttributeAdapter(
                        (MinLengthAttribute)attribute,
                        stringLocalizer);

        private static MaxLengthAttributeAdapter GetMaxLengthAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new MaxLengthAttributeAdapter(
                        (MaxLengthAttribute)attribute,
                        stringLocalizer);

        private static DataTypeAttributeAdapter GetCreditCardAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "creditcard",
                        stringLocalizer);

        private static StringLengthAttributeAdapter GetStringLengthAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new StringLengthAttributeAdapter(
                        (StringLengthAttribute)attribute,
                        stringLocalizer);

        private static RangeAttributeAdapter GetRangeAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new RangeAttributeAdapter(
                        (RangeAttribute)attribute,
                        stringLocalizer);

        private static DataTypeAttributeAdapter GetEmailAddressAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "email",
                        stringLocalizer);

        private static DataTypeAttributeAdapter GetPhoneAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "phone",
                        stringLocalizer);

        private static DataTypeAttributeAdapter GetUrlAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer) =>
                new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "url",
                        stringLocalizer);
    };
}

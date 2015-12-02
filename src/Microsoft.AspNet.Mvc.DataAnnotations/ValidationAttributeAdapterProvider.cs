using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.DataAnnotations
{
    public class ValidationAttributeAdapterProvider
    {
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

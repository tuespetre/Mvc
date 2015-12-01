using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace Microsoft.AspNet.Mvc.DataAnnotations
{
    public class ValidationAttributeAdapterTable
    {
        public static Dictionary<Type, ValidationAttributeAdapterFactory> AttributeFactories =
            new Dictionary<Type, ValidationAttributeAdapterFactory>
            {
                {
                    typeof(RegularExpressionAttribute),
                    (attribute, stringLocalizer) => new RegularExpressionAttributeAdapter(
                        (RegularExpressionAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(MaxLengthAttribute),
                    (attribute, stringLocalizer) => new MaxLengthAttributeAdapter(
                        (MaxLengthAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(MinLengthAttribute),
                    (attribute, stringLocalizer) => new MinLengthAttributeAdapter(
                        (MinLengthAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(CompareAttribute),
                    (attribute, stringLocalizer) => new CompareAttributeAdapter(
                        (CompareAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(RequiredAttribute),
                    (attribute, stringLocalizer) => new RequiredAttributeAdapter(
                        (RequiredAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(RangeAttribute),
                    (attribute, stringLocalizer) => new RangeAttributeAdapter(
                        (RangeAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(StringLengthAttribute),
                    (attribute, stringLocalizer) => new StringLengthAttributeAdapter(
                        (StringLengthAttribute)attribute,
                        stringLocalizer)
                },
                {
                    typeof(CreditCardAttribute),
                    (attribute, stringLocalizer) => new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "creditcard",
                        stringLocalizer)
                },
                {
                    typeof(EmailAddressAttribute),
                    (attribute, stringLocalizer) => new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "email",
                        stringLocalizer)
                },
                {
                    typeof(PhoneAttribute),
                    (attribute, stringLocalizer) => new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "phone",
                        stringLocalizer)
                },
                {
                    typeof(UrlAttribute),
                    (attribute, stringLocalizer) => new DataTypeAttributeAdapter(
                        (DataTypeAttribute)attribute,
                        "url",
                        stringLocalizer)
                }
            };
    };
}

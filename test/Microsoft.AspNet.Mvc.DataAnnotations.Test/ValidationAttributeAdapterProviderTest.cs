// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.DataAnnotations;
using Xunit;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    public class ValidationAttributeAdapterProviderTest
    {
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider =
            new ValidationAttributeAdapterProvider();

        public static IEnumerable<object[]> DataAnnotationAdapters
        {
            get
            {
                yield return new object[]
                {
                    new RegularExpressionAttribute("abc"),
                    typeof(RegularExpressionAttributeAdapter)
                };

                yield return new object[]
                {
                    new MaxLengthAttribute(),
                    typeof(MaxLengthAttributeAdapter)
                };

                yield return new object[]
                {
                   new MinLengthAttribute(1),
                  typeof(MinLengthAttributeAdapter)
                };

                yield return new object[]
                {
                    new RangeAttribute(1, 100),
                    typeof(RangeAttributeAdapter)
                };

                yield return new object[]
                {
                    new StringLengthAttribute(6),
                    typeof(StringLengthAttributeAdapter)
                };

                yield return new object[]
                {
                    new RequiredAttribute(),
                    typeof(RequiredAttributeAdapter)
                };
            }
        }

        [Theory]
        [MemberData(nameof(DataAnnotationAdapters))]
        public void AdapterFactory_RegistersAdapters_ForDataAnnotationAttributes(
               ValidationAttribute attribute,
               Type expectedAdapterType)
        {
            // Arrange and Act
            var adapter = _validationAttributeAdapterProvider.GetAttributeAdapter(attribute, null);

            // Assert
            Assert.IsType(expectedAdapterType, adapter);
        }

        public static IEnumerable<object[]> DataTypeAdapters
        {
            get
            {
                yield return new object[] { new UrlAttribute(), "url" };
                yield return new object[] { new CreditCardAttribute(), "creditcard" };
                yield return new object[] { new EmailAddressAttribute(), "email" };
                yield return new object[] { new PhoneAttribute(), "phone" };
            }
        }

        [Theory]
        [MemberData(nameof(DataTypeAdapters))]
        public void AdapterFactory_RegistersAdapters_ForDataTypeAttributes(
            ValidationAttribute attribute,
            string expectedRuleName)
        {
            // Arrange & Act
            var adapter = _validationAttributeAdapterProvider.GetAttributeAdapter(attribute, null);

            // Assert
            var dataTypeAdapter = Assert.IsType<DataTypeAttributeAdapter>(adapter);
            Assert.Equal(expectedRuleName, dataTypeAdapter.RuleName);
        }
    }
}

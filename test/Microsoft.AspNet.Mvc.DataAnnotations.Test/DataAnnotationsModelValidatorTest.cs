// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    public class DataAnnotationsModelValidatorTest
    {
        private static IModelMetadataProvider _metadataProvider = TestModelMetadataProvider.CreateDefaultProvider();

        [Fact]
        public void Constructor_SetsAttribute()
        {
            // Arrange
            var attribute = new RequiredAttribute();

            // Act
            var validator = new DataAnnotationsModelValidator(
                attribute,
                stringLocalizer: null);

            // Assert
            Assert.Same(attribute, validator.Attribute);
        }

        public static IEnumerable<object[]> Validate_SetsMemberName_OnValidationContext_ForProperties_Data
        {
            get
            {
                yield return new object[]
                {
                    _metadataProvider.GetMetadataForType(typeof(string)).Properties["Length"],
                    "Hello",
                    "Hello".Length,
                    "Length",
                };

                yield return new object[]
                {
                    _metadataProvider.GetMetadataForType(typeof(SampleModel)),
                    null,
                    15,
                    "SampleModel",
                };
            }
        }

        private const string LocalizationKey = "LocalizeIt";

        public static IEnumerable<object[]> Validate_AttributesIncludeValues
        {
            get
            {
                var pattern = "apattern";
                var length = 5;
                var regex = "^((?!" + pattern + ").)*$";

                yield return new object[]
                {
                    new RegularExpressionAttribute(regex) {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), regex }
                };
                yield return new object[]
                {
                    new MaxLengthAttribute(length) {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), length }
                };
                yield return new object[]
                {
                    new CompareAttribute(pattern) {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), pattern }
                };
                yield return new object[]
                {
                    new MinLengthAttribute(length) {
                        ErrorMessage = LocalizationKey
                    }, "a", new object[] { nameof(SampleModel), length }
                };
                yield return new object[]
                {
                    new CreditCardAttribute() {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), "CreditCard" }
                };
                yield return new object[]
                {
                    new StringLengthAttribute(length) {
                        MinimumLength = 1,
                        ErrorMessage = LocalizationKey
                    }, "" ,new object[] { nameof(SampleModel), length }
                };
                yield return new object[]
                {
                    new RangeAttribute(0, length) {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), 0, length}
                };
                yield return new object[]
                {
                    new EmailAddressAttribute() {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), "EmailAddress" }
                };
                yield return new object[]
                {
                    new PhoneAttribute() {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), "PhoneNumber" }
                };
                yield return new object[]
                {
                    new UrlAttribute() {
                        ErrorMessage = LocalizationKey
                    }, pattern, new object[] { nameof(SampleModel), "Url"  }
                };
            }
        }

        [Theory]
        [MemberData(nameof(Validate_SetsMemberName_OnValidationContext_ForProperties_Data))]
        public void Validate_SetsMemberName_OnValidationContext_ForProperties(
            ModelMetadata metadata,
            object container,
            object model,
            string expectedMemberName)
        {
            // Arrange
            var attribute = new Mock<TestableValidationAttribute> { CallBase = true };
            attribute
                .Setup(p => p.IsValidPublic(It.IsAny<object>(), It.IsAny<ValidationContext>()))
                .Callback((object o, ValidationContext context) =>
                {
                    Assert.Equal(expectedMemberName, context.MemberName);
                })
                .Returns(ValidationResult.Success)
                .Verifiable();
            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Container = container,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var results = validator.Validate(validationContext);

            // Assert
            Assert.Empty(results);
            attribute.VerifyAll();
        }

        [Fact]
        public void Validate_Valid()
        {
            // Arrange
            var metadata = _metadataProvider.GetMetadataForType(typeof(string));
            var container = "Hello";
            var model = container.Length;

            var attribute = new Mock<ValidationAttribute> { CallBase = true };
            attribute.Setup(a => a.IsValid(model)).Returns(true);

            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Container = container,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var result = validator.Validate(validationContext);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Validate_Invalid()
        {
            // Arrange
            var metadata = _metadataProvider.GetMetadataForProperty(typeof(string), "Length");
            var container = "Hello";
            var model = container.Length;

            var attribute = new Mock<ValidationAttribute> { CallBase = true };
            attribute.Setup(a => a.IsValid(model)).Returns(false);

            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Container = container,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var result = validator.Validate(validationContext);

            // Assert
            var validationResult = result.Single();
            Assert.Equal("", validationResult.MemberName);
            Assert.Equal(attribute.Object.FormatErrorMessage("Length"), validationResult.Message);
        }

        [Fact]
        public void Validatate_ValidationResultSuccess()
        {
            // Arrange
            var metadata = _metadataProvider.GetMetadataForType(typeof(string));
            var container = "Hello";
            var model = container.Length;

            var attribute = new Mock<TestableValidationAttribute> { CallBase = true };
            attribute
                .Setup(p => p.IsValidPublic(It.IsAny<object>(), It.IsAny<ValidationContext>()))
                .Returns(ValidationResult.Success);
            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Container = container,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var result = validator.Validate(validationContext);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Validate_ReturnsSingleValidationResult_IfMemberNameSequenceIsEmpty()
        {
            // Arrange
            const string errorMessage = "Some error message";

            var metadata = _metadataProvider.GetMetadataForType(typeof(string));
            var container = "Hello";
            var model = container.Length;

            var attribute = new Mock<TestableValidationAttribute> { CallBase = true };
            attribute
                 .Setup(p => p.IsValidPublic(It.IsAny<object>(), It.IsAny<ValidationContext>()))
                 .Returns(new ValidationResult(errorMessage, memberNames: null));
            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);

            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Container = container,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var results = validator.Validate(validationContext);

            // Assert
            var validationResult = Assert.Single(results);
            Assert.Equal(errorMessage, validationResult.Message);
            Assert.Empty(validationResult.MemberName);
        }

        [Fact]
        public void Validate_ReturnsSingleValidationResult_IfOneMemberNameIsSpecified()
        {
            // Arrange
            const string errorMessage = "A different error message";

            var metadata = _metadataProvider.GetMetadataForType(typeof(object));
            var model = new object();

            var attribute = new Mock<TestableValidationAttribute> { CallBase = true };
            attribute
                 .Setup(p => p.IsValidPublic(It.IsAny<object>(), It.IsAny<ValidationContext>()))
                 .Returns(new ValidationResult(errorMessage, new[] { "FirstName" }));

            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var results = validator.Validate(validationContext);

            // Assert
            ModelValidationResult validationResult = Assert.Single(results);
            Assert.Equal(errorMessage, validationResult.Message);
            Assert.Equal("FirstName", validationResult.MemberName);
        }

        [Fact]
        public void Validate_ReturnsMemberName_IfItIsDifferentFromDisplayName()
        {
            // Arrange
            var metadata = _metadataProvider.GetMetadataForType(typeof(SampleModel));
            var model = new SampleModel();

            var attribute = new Mock<TestableValidationAttribute> { CallBase = true };
            attribute
                 .Setup(p => p.IsValidPublic(It.IsAny<object>(), It.IsAny<ValidationContext>()))
                 .Returns(new ValidationResult("Name error", new[] { "Name" }));

            var validator = new DataAnnotationsModelValidator(
                attribute.Object,
                stringLocalizer: null);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            var results = validator.Validate(validationContext);

            // Assert
            ModelValidationResult validationResult = Assert.Single(results);
            Assert.Equal("Name", validationResult.MemberName);
        }

        [Fact]
        public void Validate_IsValidFalse_StringLocalizerReturnsLocalizerErrorMessage()
        {
            // Arrange
            var metadata = _metadataProvider.GetMetadataForType(typeof(string));
            var container = "Hello";

            var attribute = new MaxLengthAttribute(4);
            attribute.ErrorMessage = "Length";

            var localizedString = new LocalizedString("Length", "Longueur est invalide : 4");
            var stringLocalizer = new Mock<IStringLocalizer>();
            stringLocalizer.Setup(s => s["Length", It.IsAny<object[]>()]).Returns(localizedString);

            var validator = new DataAnnotationsModelValidator(
                attribute,
                stringLocalizer.Object);
            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Container = container,
                Model = "abcde",
                MetadataProvider = _metadataProvider
            };

            // Act
            var result = validator.Validate(validationContext);

            // Assert
            var validationResult = result.Single();
            Assert.Equal("", validationResult.MemberName);
            Assert.Equal("Longueur est invalide : 4", validationResult.Message);
        }

        [Theory]
        [MemberData(nameof(Validate_AttributesIncludeValues))]
        public void Validate_IsValidFalse_StringLocalizerGetsArguments(
            ValidationAttribute attribute,
            string model,
            object[] values)
        {
            // Arrange
            var stringLocalizer = new Mock<IStringLocalizer>();

            var validator = new DataAnnotationsModelValidator(
                attribute,
                stringLocalizer.Object);

            var metadata = _metadataProvider.GetMetadataForType(typeof(SampleModel));

            var validationContext = new ModelValidationContext()
            {
                ModelMetadata = metadata,
                Model = model,
                MetadataProvider = _metadataProvider
            };

            // Act
            validator.Validate(validationContext);

            // Assert
            stringLocalizer.Verify(l => l[LocalizationKey, values]);
        }

        public abstract class TestableValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                return IsValidPublic(value, validationContext);
            }

            public abstract ValidationResult IsValidPublic(object value, ValidationContext validationContext);
        }

        private class SampleModel
        {
            public string Name { get; set; }
        }
    }
}

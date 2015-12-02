// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.AspNet.Mvc.IntegrationTests
{
    [ProductValidator]
    public class Product
    {
        public string Name { get; set; }

        [StringLength(20)]
        [RegularExpression("^[0-9]*$")]
        [Display(Name = "Contact Us")]
        public string Contact { get; set; }

        [Range(0, 100)]
        public virtual int Price { get; set; }

        [CompanyName]
        public string CompanyName { get; set; }

        public string Country { get; set; }

        [Required]
        public ProductDetails ProductDetails { get; set; }
    }

    [ModelMetadataType(typeof(Software))]
    public class SoftwareViewModel
    {
        [RegularExpression("^[0-9]*$")]
        public string Version { get; set; }

        public DateTime DatePurchased { get; set; }

        public int Price { get; set; }

        public string Contact { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string CompanyName { get; set; }
    }

    public class ProductValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var product = value as ProductViewModel;
            if (product != null)
            {
                if (!product.Country.Equals("USA") || string.IsNullOrEmpty(product.Name))
                {
                    return new ValidationResult("Product must be made in the USA if it is not named.");
                }
                else
                {
                    return null;
                }
            }
            var software = value as SoftwareViewModel;
            if (software != null)
            {
                if (!software.Country.Equals("USA") || string.IsNullOrEmpty(software.Name))
                {
                    return new ValidationResult("Product must be made in the USA if it is not named.");
                }
                else
                {
                    return null;
                }
            }

            return new ValidationResult("Expected either ProductViewModel or SoftwareViewModel instance but got "
                + value.GetType() + " instance");
        }
    }

    public class CompanyNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueString = value as string;
            if (string.IsNullOrEmpty(valueString))
            {
                return new ValidationResult("CompanyName cannot be null or empty.");
            }

            return null;
        }
    }

    public class Software : Product
    {
        public string Version { get; set; }

        [Required]
        public DateTime DatePurchased { get; set; }

        [Range(100, 200)]
        public override int Price { get; set; }

        [StringLength(10)]
        public new string Contact { get; set; }
    }

    public class ProductDetails
    {
        [Required]
        public string Detail1 { get; set; }

        [Required]
        public string Detail2 { get; set; }

        [Required]
        public string Detail3 { get; set; }
    }

    [ModelMetadataType(typeof(Product))]
    public class ProductViewModel
    {
        public string Name { get; set; }

        [Required]
        public string Contact { get; set; }

        [Range(20, 100)]
        public int Price { get; set; }

        [RegularExpression("^[a-zA-Z]*$")]
        [Required]
        public string Category { get; set; }

        public string CompanyName { get; set; }

        public string Country { get; set; }

        public ProductDetails ProductDetails { get; set; }
    }
}

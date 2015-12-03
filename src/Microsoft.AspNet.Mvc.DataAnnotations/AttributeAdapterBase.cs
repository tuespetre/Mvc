// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.DataAnnotations
{
    /// <summary>
    /// An extension of <see cref="ValidationAttributeAdapter{TAttribute}"/> which forces adapters to implement
    /// GetErrorMessage while providing their relevent values.
    /// </summary>
    /// <typeparam name="TAttribute">The type of <see cref="ValidationAttribute"/> which is being wrapped.</typeparam>
    public abstract class AttributeAdapterBase<TAttribute> :
        ValidationAttributeAdapter<TAttribute>,
        IAttributeAdapter
        where TAttribute : ValidationAttribute
    {
        public AttributeAdapterBase(TAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Overload which should provide this attributes important values to the base GetErrorMessage.
        /// </summary>
        /// <param name="validationContext">The context for which to create an error message.</param>
        /// <returns>The localized error message.</returns>
        public abstract string GetErrorMessage(ModelValidationContextBase validationContext);
    }
}

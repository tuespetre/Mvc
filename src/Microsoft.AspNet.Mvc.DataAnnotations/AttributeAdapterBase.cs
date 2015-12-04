// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    /// <summary>
    /// An abstract subclass of <see cref="ValidationAttributeAdapter{TAttribute}"/> which implements 
    /// <see cref="IAttributeAdapter"/>.
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

        /// <inheritdoc/>
        public abstract string GetErrorMessage(ModelValidationContextBase validationContext);
    }
}

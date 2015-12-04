// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    /// <summary>
    /// Interface so that adapters provide their relevent values to error messages.
    /// </summary>
    public interface IAttributeAdapter : IClientModelValidator
    {
        /// <summary>
        /// Implementors should provide the important values of their attribute to the base
        /// <see cref="GetErrorMessage(ModelValidationContextBase)"/>.
        /// </summary>
        /// <param name="validationContext">The context to use in message creation.</param>
        /// <returns>The localized and parameterized error message.</returns>
        string GetErrorMessage(ModelValidationContextBase validationContext);
    }
}

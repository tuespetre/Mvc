using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNet.Mvc.DataAnnotations
{
    public interface IAttributeAdapter : IClientModelValidator
    {
        string GetErrorMessage(ModelMetadata modelMetadata, IModelMetadataProvider metadataProvider);
    }

    public abstract class AttributeAdapterBase<TAttribute> :
        ValidationAttributeAdapter<TAttribute>,
        IAttributeAdapter
        where TAttribute : ValidationAttribute
    {
        public AttributeAdapterBase(TAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        { }

        public abstract string GetErrorMessage(ModelMetadata modelMetadata, IModelMetadataProvider metadataProvider);
    }
}

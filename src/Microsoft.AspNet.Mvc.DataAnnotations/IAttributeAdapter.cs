using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace Microsoft.AspNet.Mvc.DataAnnotations
{
    public interface IAttributeAdapter : IClientModelValidator
    {
        string GetErrorMessage(ModelMetadata modelMetadata, IModelMetadataProvider metadataProvider);
    }
}

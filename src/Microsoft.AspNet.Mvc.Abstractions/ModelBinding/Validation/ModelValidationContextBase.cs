using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    /// <summary>
    /// A common base class for <see cref="ModelValidationContext"/> and <see cref="ClientModelValidationContext"/>.
    /// </summary>
    public class ModelValidationContextBase
    {
        /// <summary>
        /// Gets or sets the <see cref="Mvc.ActionContext"/>
        /// </summary>
        public ActionContext ActionContext { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ModelBinding.ModelMetadata"/> associated with <see cref="Model"/>.
        /// </summary>
        public ModelMetadata ModelMetadata { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IModelMetadataProvider"/> 
        /// </summary>
        public IModelMetadataProvider MetadataProvider { get; set; }
    }
}

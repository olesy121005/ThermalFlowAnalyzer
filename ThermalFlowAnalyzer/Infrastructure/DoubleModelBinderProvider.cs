using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThermalFlowAnalyzer.Infrastructure
{
    public class DoubleModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(double) ||
                context.Metadata.ModelType == typeof(double?))
            {
                return new DoubleModelBinder();
            }

            return null;
        }
    }
}

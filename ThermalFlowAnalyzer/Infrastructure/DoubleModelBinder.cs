using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace ThermalFlowAnalyzer.Infrastructure
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var value = valueResult.FirstValue;
            if (string.IsNullOrWhiteSpace(value))
                return Task.CompletedTask;

            value = value.Replace(',', '.');

            if (double.TryParse(
                value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName,
                    "Введите корректное числовое значение"
                );
            }

            return Task.CompletedTask;
        }
    }
}

using Microsoft.OpenApi.Models;
using Project.Core.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project.Core.Utilities.SwaggerOperationFilters
{
    public class TableValuedFunctionRequestOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if any endpoint has body with TableValuedFunctionRequest type
            var hasProductBodyParam = context.MethodInfo.GetParameters()
                .Any(param => param.ParameterType == typeof(TableValuedFunctionRequest) || param.ParameterType == typeof(PeriodicTableValuedFunctionRequest));

            if (hasProductBodyParam)
            {
                // Add a description to the operation for Swagger
                operation.Description += $"{nameof(ColumnFilterType)}: ";
                
                foreach (ColumnFilterType columnFilterType in Enum.GetValues(typeof(ColumnFilterType)))
                {
                    int value = (int)columnFilterType;
                    string name = columnFilterType.ToString();

                    operation.Description += $"{value}-{name}. ";
                }
            }
        }
    }
}

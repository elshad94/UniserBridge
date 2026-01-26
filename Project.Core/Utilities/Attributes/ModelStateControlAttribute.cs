using Project.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
/// <summary>
/// Controller içərisində  metodların (Action) üzərinə attribute olaraq yazılır, əgər xüsusi sahələr varsa xeta mesaji döndərir
/// </summary>
/// 
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ModelStateControlAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {

        if (!context.ModelState.IsValid)
        {
            List<string> invalidFields = new List<string>();

            foreach (var item in context.ModelState)
            {
                invalidFields.Add(item.Key.Replace("$.", ""));
            }

            context.Result = new BadRequestObjectResult(
                ResultDataGenerator.Generate(new Result(new { invalidFields }, ResultInfo.FillRequiredFields)));
        }
        base.OnActionExecuting(context);
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Poupe.API.Extensions;

public class CustomInvalidModelError
{
    public BadRequestObjectResult CustomErrorResponse(ActionContext context)
    {
        Dictionary<string, List<string>> jsonResult = new();

        context.ModelState
            .Where(modelError => modelError.Value!.Errors.Count > 0)
            .Select(modelError =>
            {
                var keyName = modelError.Key;
                var errors = modelError.Value?.Errors.Select(e => e.ErrorMessage).ToList();
                var unexpectedErrors = errors!.Where(e => e.Contains("is not valid") || e.Contains("is invalid")).ToList();

                if (modelError.Key.StartsWith("$.") || unexpectedErrors.Any())
                {
                    keyName = unexpectedErrors.Any() ? keyName : keyName[2].ToString().ToUpper() + keyName[3..];
                    errors = new List<string> { "Valor inserido no campo é inválido" };
                }
                jsonResult.Add(keyName, errors!);
                return string.Empty;

            }).ToList();

        var objectResult = new
        {
            message = "Requisição inválida",
            erros = jsonResult.Select(entry => new
            {
                field = entry.Key,
                messages = entry.Value
            }).ToList()
        };

        return new BadRequestObjectResult(objectResult);
    }
}
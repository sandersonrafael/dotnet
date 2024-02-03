using FinalProject.Domain.Products;
using FinalProject.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoints.Categories;

public class CategoryUpdate
{
    // To validate route params, can be used :type notation. Ex:
    // Using this, the API automatically will send 404 respnose if user's input is invalid
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest request, DatabaseContext context)
    {
        Category? category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return Results.NotFound();

        category.EditInfo(request.Name, request.Active);

        if (!category.IsValid) return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.SaveChanges();

        return Results.Ok(category);
    }
}

using FinalProject.Domain.Products;
using FinalProject.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoints.Categories;

public class CategoryUpdate
{
    public static string Template => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest request, DatabaseContext context)
    {
        Category? category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return Results.NotFound();
        
        category.Name = request.Name;
        category.Active = request.Active;

        context.SaveChanges();

        return Results.Ok(category);
    }
}

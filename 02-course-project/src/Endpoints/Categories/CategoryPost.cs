using FinalProject.Domain.Products;
using FinalProject.Infra.Data;

namespace FinalProject.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryRequest request, DatabaseContext context)
    {
        Category category = new()
        {
            Name = request.Name,
            CreatedBy = "Test",
            CreatedOn = DateTime.UtcNow,
            EditedBy = "Test",
            EditedOn = DateTime.UtcNow
        };
        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"{Template}/{category.Id}", category);
    }
}

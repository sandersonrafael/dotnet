using FinalProject.Domain.Products;
using FinalProject.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    //[AllowAnonymous] // All users can access this route and get their resources without authentication / authorization
    [Authorize] // Only authenticated / authorized users can access this endpoint
    public static IResult Action(CategoryRequest request, DatabaseContext context)
    {
        Category category = new(request.Name/*, string.Empty*/, "Test", "Test");

        // Flunt options after creating notificatoins constructor for Category entity
        if (!category.IsValid) // Handling Errors
        {
            /* This code was moved to a class named ProblemDetailsExtensions with an extension method named ConvertToProblemDetails */

            // It will group by notification keys, like "name" and attrib all errors grouped by that key
            // var errors = category.Notifications.GroupBy(not => not.Key)
            //     .ToDictionary(not => not.Key, not => not.Select(mes => mes.Message).ToArray());

            Dictionary<string, string[]> errors = category.Notifications.ConvertToProblemDetails();
            return Results.ValidationProblem(errors);
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"{Template}/{category.Id}", category);
    }
}

using FinalProject.Endpoints.Categories;
using FinalProject.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    // For user entities, we don't use the class DbContext to inject. Instead, use UserManager<IdentityUser> userManager, like:
    public static IResult Action(EmployeeRequest request, UserManager<IdentityUser> userManager)
    {
        IdentityUser user = new() { UserName = request.Email, Email = request.Email, };
        var result = userManager.CreateAsync(user).Result;

        if (!result.Succeeded) return Results.BadRequest(result.Errors.First());

        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}

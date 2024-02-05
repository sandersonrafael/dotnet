using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

        // To add aditional properties to an Identity user from Identity package, it's necessary to use and add Claims:
        //IdentityResult claimResult = userManager.AddClaimAsync(user, new Claim("EmployeeCode", request.EmployeeCode)).Result;
        //if (!claimResult.Succeeded) return Results.BadRequest(claimResult.Errors.First());

        /* Adding name, that don't exists in an Identity */
        //claimResult = userManager.AddClaimAsync(user, new Claim("Name", request.Name)).Result;
        //if (!claimResult.Succeeded) return Results.BadRequest(claimResult.Errors.First());

        /* Adding all Claims in one time */
        List<Claim> userClaims = [new("EmployeeCode", request.EmployeeCode), new("Name", request.Name)];
        IdentityResult claimResult = userManager.AddClaimsAsync(user, userClaims).Result;
        if (!claimResult.Succeeded)
        {
            Dictionary<string, string[]> identityErrors = claimResult.Errors.ConvertToProblemDetails();
            return Results.ValidationProblem(identityErrors);
        }

        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}

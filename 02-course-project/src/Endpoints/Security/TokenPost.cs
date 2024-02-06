using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    // For user entities, we don't use the class DbContext to inject. Instead, use UserManager<IdentityUser> userManager, like:
    [AllowAnonymous] // All users can access this endpoint to get a bearer token if authenticated
    public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        IdentityUser? user = userManager.FindByEmailAsync(loginRequest.Email).Result;

        if (user == null) Results.BadRequest();
        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result) Results.BadRequest();

        /* Generating JWT */
        var key = Encoding.ASCII.GetBytes("aDefinedByUserKey@Pjkn@&6d6@bihbidb2@");

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, loginRequest.Email)
            }),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "FinalProject",
            Issuer = "Issuer"
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return Results.Ok(new { token = tokenHandler.WriteToken(token) });
    }
}

using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace FinalProject.Endpoints.Employees;

public class EmployeeFindAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // For user entities, we don't use the class DbContext to inject. Instead, use UserManager<IdentityUser> userManager, like:
    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        // It will be use Dapper ORM package to join them both tables and return a user data
        // Or use querys in general
        var db = new SqlConnection(configuration.GetConnectionString("Default"));
        string query = @"SELECT Email, ClaimValue as Name
            FROM AspNetUsers u INNER JOIN AspNetUserClaims c
            ON u.Id = c.UserId
            AND ClaimType = 'Name'
            ORDER BY Name";

        IEnumerable<EmployeeResponse> employees;
        if (page != null && rows != null)
        {
            // add variables to query
            query += "OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            employees = db.Query<EmployeeResponse>(query, new { page, rows });

        }
        else employees = db.Query<EmployeeResponse>(query);

        return Results.Ok(employees);
    }
}

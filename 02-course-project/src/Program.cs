using FinalProject.Endpoints.Categories;
using FinalProject.Endpoints.Employees;
using FinalProject.Endpoints.Security;
using FinalProject.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// The SQL Service will be inject in all calls of DatabaseContext
builder.Services.AddSqlServer<DatabaseContext>(builder.Configuration["ConnectionStrings:Default"]);

// Add Identity has a service of application
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => // To change rules about the Identity Entity like password validations:
{
    options.Password.RequireUppercase = false; // The password don't require uppercase
    options.Password.RequiredLength = 1; // The minimum length of the password now is 1
}).AddEntityFrameworkStores<DatabaseContext>();

// To use authorization services, it is necessary to add this services on builder ( JWT )
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"] ?? "")
        ),
    };
});
builder.Services.AddAuthorization(options => // To add a request authorization by Bearer token by default in all routes, use:
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// To App Use the authorization / authentication with jwt bearer token
app.UseAuthentication();
app.UseAuthorization();

// Swagger is before the authentication / authorization because it will used in development. Don't need auth
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryFindAll.Template, CategoryFindAll.Methods, CategoryFindAll.Handle);
app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
app.MapMethods(CategoryUpdate.Template, CategoryUpdate.Methods, CategoryUpdate.Handle);

app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);
app.MapMethods(EmployeeFindAll.Template, EmployeeFindAll.Methods, EmployeeFindAll.Handle);

app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);

app.Run();

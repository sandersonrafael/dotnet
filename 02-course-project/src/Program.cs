using FinalProject.Endpoints.Categories;
using FinalProject.Endpoints.Employees;
using FinalProject.Infra.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// The SQL Service will be inject in all calls of DatabaseContext
builder.Services.AddSqlServer<DatabaseContext>(builder.Configuration["ConnectionStrings:Default"]);

// Add Identity has a service of application
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => // To change rules about the Identity Entity like password validations:
{
    options.Password.RequireUppercase = false; // The password don't require upercase
    options.Password.RequiredLength = 1; // The minimum length of the password now is 1
}).AddEntityFrameworkStores<DatabaseContext>();

var app = builder.Build();

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

app.Run();

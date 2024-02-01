using _01_first_project.Models;
using _01_first_project.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World test with command 'dotnet watch run'");
app.MapGet("/user", () => new { Name = "Sanderson Rafael", Age = 26 }); // Anonymous object

// It's possible to use the Request and Response objects like:
app.MapPost("/add-header", (HttpRequest request, HttpResponse response) =>
{
    response.Headers.Append("Auth", "Authorized");
    return "Header added";
});

app.MapPost("/save-product", (Product product) => Results.Ok(product));

// Route params

// Query params -> It is necessary to use the annotation [FromQuery]
app.MapGet("/get-product", ([FromQuery] string? dateStart, [FromQuery] string? dateEnd) => $"dateStart: {dateStart}, dateEnd: {dateEnd}");

// PathVariables -> (optional) use annotation [FromRoute]
app.MapGet("/get-product/{code}", ([FromRoute] string code) => $"code: {code}");

app.MapGet("/get-product-header", (HttpRequest request) => /* request.Headers.Authorization */ /* || */ request.Headers["Authorization"][0]);


/* CRUD */

app.MapGet("/products", () => ProductRepository.FindAll());
app.MapGet("/products/{code}", ([FromRoute] string code) => ProductRepository.FindByCode(code));
app.MapPost("/products", (Product product) => ProductRepository.Create(product));
app.MapPut("/products/{code}", ([FromRoute] string code, Product product) => ProductRepository.Update(code, product));

app.Run();

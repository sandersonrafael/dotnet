using _01_first_project.Models;
using _01_first_project.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configuration = app.Configuration; // Obtain configuration from appsettings.json

// Inject configuration from application into ProductRepository class, initializing the list
ProductRepository.Init(configuration);



/* Basics From Http Methods */

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

app.MapGet("/products", () => Results.Ok(ProductRepository.FindAll()));
app.MapGet("/products/{code}", ([FromRoute] string code) => Results.Ok(ProductRepository.FindByCode(code)));
app.MapPost("/products", (Product product) =>
{
    ProductRepository.Create(product);
    return Results.Created();
});
app.MapPut("/products/{code}", ([FromRoute] string code, Product product) => ProductRepository.Update(code, product));
app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    ProductRepository.Delete(code);
    return Results.NoContent();
});


/* Showing configuration in a http method */

// using this notation: variable["firstPath:secondPath"], we can access the items inside a object with nested attributes

if (app.Environment.IsStaging()) // Example using the Environment type in a condition
    app.MapGet("/configuration/database", (IConfiguration configuration) =>
        Results.Ok($"Connection: {configuration["database:connection"]}, Port: {configuration["database:port"]}"));

app.Run();

using _01_first_project.Context;
using _01_first_project.Models.Dtos;
using _01_first_project.Models.Entities;
using _01_first_project.Models.Exceptions;
using _01_first_project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add auto injection for AppDbContext class
//builder.Services.AddDbContext<AppDbContext>();

// Auto inject the SQLServer ConnectionString from appsettings.json
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration["ConnectionStrings:Default"]);

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

/* Before Connect With Database */
//app.MapGet("/products", () => Results.Ok(ProductRepository.FindAll()));

/* After Connect With Database */
// The Include method add the another entities that are relationed with Products
app.MapGet("/products", (AppDbContext context, HttpRequest httpRequest) =>
{
    try
    {                               // It will throws exception because int product.Category.Id don't exists inside Product class
        return Results.Ok(context.Products.Include(p => p.Category.Id).Include(p => p.Tags).ToList());
    }
    catch (Exception e)
    {
        // Exception handling example
        return Results.BadRequest(new ExceptionResponse(httpRequest.Method, httpRequest.Path.Value ?? "", e.Message));
    }
});

/* Before Connect With Database */
//app.MapGet("/products/{code}", ([FromRoute] string code) => Results.Ok(ProductRepository.FindByCode(code)));

/* After Connect With Database */
app.MapGet("/products/{id}", ([FromRoute] int id, AppDbContext context) =>
{
    Product? product = context.Products
        .Include(p => p.Category) // Necessary to take the Categories from database and relationate with this product
        .Include(p => p.Tags) // Necessary to take the Tags from database and relationate with this product
        .SingleOrDefault(p => p.Id == id);
    if (product == null) return Results.NotFound(null);
    return Results.Ok(product);
});


/* Before Connect With Database */
//app.MapPost("/products", (Product product) =>
//{
//    ProductRepository.Create(product);
//    return Results.Created();
//});

/* After Connect With Database */
app.MapPost("/products", (ProductRequest request, AppDbContext context) => // AppDbContext injected automatically 
{
    Category category = context.Categories.Where(c => c.Id == request.CategoryId).First();
    Product product = new() {
        Code = request.Code,
        Name = request.Name,
        Description = request.Description,
        Category = category,
        Tags = []
    };
    if (request.Tags is not null) request.Tags.ForEach(tag => product.Tags.Add(new Tag { Name = tag }));

    context.Products.Add(product);
    context.SaveChanges();
    return Results.Created($"/products/{product.Id}", product);
});

/* Before Connect With Database */
//app.MapPut("/products/{code}", ([FromRoute] string code, Product product) => ProductRepository.Update(code, product));

/* After Connect With Database */
app.MapPut("/products/{id}", ([FromRoute] int id, ProductRequest request, AppDbContext context) =>
{
    Product? product = context.Products
        .Include(p => p.Category)
        .Include(p => p.Tags)
        .SingleOrDefault(p => p.Id == id);
    if (product == null) return Results.NotFound();

    if (request?.CategoryId != null) 
    { 
        Category? category = context.Categories.SingleOrDefault(cat => cat.Id == request.CategoryId);
        if (category != null) product.Category = category;
    }

    if (request?.Code != null) product.Code = request.Code;
    if (request?.Name != null) product.Name = request.Name;
    if (request?.Description != null) product.Description = request.Description;

    if (request.Tags != null)
    {
        product.Tags = new List<Tag>();
        request.Tags.ForEach(tag => product.Tags.Add(new Tag { Name = tag }));
    }

    context.SaveChanges();

    return Results.Ok(product);
});

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

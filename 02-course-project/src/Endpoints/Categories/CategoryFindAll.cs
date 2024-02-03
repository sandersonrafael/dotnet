﻿using FinalProject.Domain.Products;
using FinalProject.Infra.Data;

namespace FinalProject.Endpoints.Categories;

public class CategoryFindAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(DatabaseContext context) =>
        Results.Ok(context.Categories.Select(c => new CategoryResponse() { Id = c.Id, Name = c.Name, Active = c.Active }));
}

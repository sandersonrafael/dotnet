using _01_first_project.Models.Entities;

namespace _01_first_project.Repositories;

public static class ProductRepository
{
    private static List<Product> Products = [];

    public static void Init(IConfiguration configuration)
    {
        // That will get the configuration with name "Products" from appsettings.json file and convert to described type
        List<Product>? products = configuration.GetSection("Products").Get<List<Product>>();
        Products = products ?? [];
    }

    public static List<Product> FindAll() => Products;

    public static Product? FindByCode(string code) => Products.FirstOrDefault(p => p.Code == code);

    public static Product? Create(Product product)
    {
        if (Products.Any(p => p.Code == product.Code)) return null;
        Products.Add(product);
        return product;
    }

    public static Product? Update(string code, Product product)
    {
        int foundIndex = Products.FindIndex(p => p.Code == code);
        if (foundIndex == -1) return null;
        Products[foundIndex].Name = product.Name;
        return Products[foundIndex];
    }

    public static void Delete(string code) => Products.RemoveAll(p => p.Code == code);
}

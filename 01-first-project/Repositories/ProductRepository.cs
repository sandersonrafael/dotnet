using _01_first_project.Models;

namespace _01_first_project.Repositories;

public static class ProductRepository
{
    private static readonly List<Product> Products = [];

    public static List<Product> FindAll() => Products;

    public static Product? FindByCode(string code) => Products.FirstOrDefault(p => p.Code == code);

    public static Product? Create(Product product)
    {
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

    public static void Delete(Product product) => Products.Remove(product);
}

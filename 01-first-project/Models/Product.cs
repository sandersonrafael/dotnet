namespace _01_first_project.Models;

public class Product(string code, string name)
{
    public string Code { get; set; } = code;
    public string Name { get; set; } = name;

    public override bool Equals(object? obj)
    {
        if (obj is not Product) throw new ArgumentException("Invalid object to compare");
        Product other = (Product)obj;
        return Code.Equals(other.Code);
    }

    public override int GetHashCode() => Code.GetHashCode();
}

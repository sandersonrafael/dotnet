using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_first_project.Models;

[Table("product")]
public class Product(string code, string name)
{
    [Key]
    [Column("code", TypeName = "VARCHAR(255)")]
    public string Code { get; set; } = code;

    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; } = name;

    public override bool Equals(object? obj)
    {
        if (obj is not Product) throw new ArgumentException("Invalid object to compare");
        Product other = (Product)obj;
        return Code.Equals(other.Code);
    }

    public override int GetHashCode() => Code.GetHashCode();
}

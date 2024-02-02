using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_first_project.Models;

[Table("product")]
public class Product
{
    [Key]
    //[Column("id")]
    public int Id { get; set; }

    //[Column("code", TypeName = "VARCHAR(255)")]
    public string Code { get; set; }

    //[Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; }

    //[Column("description", TypeName = "VARCHAR(255)")]
    public string Description { get; set; }

    //[Column("category_id")]
    // One to One relationship
    [Required]
    public Category Category { get; set; }

    // One to Many relationship
    public List<Tag> Tags { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Product) throw new ArgumentException("Invalid object to compare");
        Product other = (Product)obj;
        return Code.Equals(other.Code);
    }

    public override int GetHashCode() => Code.GetHashCode();
}

namespace _01_first_project.Models.Entities;

public class Category(int id, string name)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}

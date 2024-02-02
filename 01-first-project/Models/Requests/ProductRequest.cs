namespace _01_first_project.Models.Dtos;

public record ProductRequest(string Code, string Name, string Description, int CategoryId, List<string> Tags);

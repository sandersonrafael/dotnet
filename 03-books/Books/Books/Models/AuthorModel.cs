using System.Text.Json.Serialization;

namespace Books.Models;

public class AuthorModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore]
    public ICollection<BookModel> Books { get; set; } = [];
}

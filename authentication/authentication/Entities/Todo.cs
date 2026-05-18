using System.Text.Json.Serialization;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;

    public Guid UserId { get; set; }

    [JsonIgnore] // Client DOES NOT send User, Server DOES NOT expect User, EF Core still uses it internally. another option is make it nullable public User? User { get; set; }
    public User? User { get; set; }
}
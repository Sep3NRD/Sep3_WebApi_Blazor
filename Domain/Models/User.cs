using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    [JsonIgnore]
    //public List<Post> Posts { get; set; } = new List<Post>();
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
namespace Ploomes.LiveCode.WebApi.Models;

#nullable disable
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}

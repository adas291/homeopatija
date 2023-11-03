
namespace homeopatija.Models;

public class Comment
{
  public int Id { get; set; }
  public string Author { get; set; }
  public string Body { get; set; }
  public DateTime CreationTime { get; set; } = DateTime.Now;

}
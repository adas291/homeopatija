using NuGet.Common;

namespace homeopatija.Entities;

public class Comment
{
  public int Id { get; set; }
  public DateTime Date{ get; set; }
  public string Body{ get; set; }
  public int UserId { get; set; }
  public User User { get; set; }
}
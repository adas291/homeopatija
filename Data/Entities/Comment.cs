
namespace homeopatija.Entities;

public class Comment
{
  public int Id { get; set; }
  public string Body { get; set; }
  public int UserId { get; set; }
  public User User { get; set; }
  public DateTime CreationTime { get; set; } = DateTime.Now;
  public int DrugId { get; set; }
  public Drug? Drug { get; set; }
}
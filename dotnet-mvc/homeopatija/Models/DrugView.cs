namespace homeopatija.Models;

public class DrugView
{
  public Drug Drug { get; set; }
  public List<Comment> Comments { get; set; } = new List<Comment>();
}
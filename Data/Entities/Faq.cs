namespace homeopatija.Entities;

public class FAQ
{
  public int Id { get; set; }
  public string Question { get; set; }
  public string Answer { get; set; }
  public DateTime CreationDate { get; set; }
  public int OrderId { get; set; }
}

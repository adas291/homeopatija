namespace homeopatija.Entities;

public enum UserType
{
  Client,
  Admin,
  Druggist
}

public class User
{
  public int Id { get; set; }
  public string Name{ get; set; }
  public string Surname { get; set; }
  public string Passowrd{ get; set; }
  public string Phone { get; set; }
  public string Address { get; set; }
  public UserType UserType { get; set; }
  public DateTime CreationDate { get; set; }

}
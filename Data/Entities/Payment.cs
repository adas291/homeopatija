namespace homeopatija.Entities;


public class Payment
{
  public int Id { get; set; }
  public DateTime MyProperty { get; set; }
  public int OrderId { get; set; }
  public Order Order { get; set; }
  public int PaymentTypeId { get; set; }
  public PaymentMethod PaymentType { get; set; }
}

public class PaymentMethod
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string FullName { get; set; }
  public string LogoUrl { get; set; }
  public int MinPaymentCount { get; set; }
  public int MaxPaymentCount { get; set; }
}
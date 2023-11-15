namespace homeopatija.Entities;



public enum DeliveryType
{
  InShop,
  Courier,
  PostMachine
}

public enum OrderStatus
{
  Initialized,
  PaymentReceived,
  Sent,
  WaitingDeliver,
  Delivered

}

public class Order
{
  public int Id{ get; set; }
  public DateTime Date{ get; set; }
  public OrderStatus OrderStatus { get; set; }
  public DeliveryType DeliveryType { get; set; }
}


public class OrderDrug
{
  public int Id { get; set; }
  public int Count { get; set; }
  public int OrderId { get; set; }
  public Order Order { get; set; }
  public int DrugId { get; set; }
  public Drug Drug{ get; set; }
}


public class CartDrug
{
  public int Id { get; set; }
  public int Count { get; set; }
  public int OrderId { get; set; }
  public Order Order { get; set; }
  public int DrugId { get; set; }
  public Drug Drug{ get; set; }
}
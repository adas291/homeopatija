namespace homeopatija.Data.Dtos;

public class ShoppingCartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ShoppingCartViewModel
{
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
}

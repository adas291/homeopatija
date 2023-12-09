namespace homeopatija.Dtos;
using homeopatija.Entities;

public class DrugDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = "";
    public int AvailableStock { get; set; }
    public int OrderedStock { get; set; }
}

public class DrugIndexDto
{
    public List<DrugDto> Drugs { get; set; }
}

public class DrugViewDto
{
    public Drug Drug { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
}

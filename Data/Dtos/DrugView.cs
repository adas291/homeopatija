using homeopatija.Entities;

namespace homeopatija.Data.Dtos;

public class DrugViewDto
{
    public Drug Drug { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
}

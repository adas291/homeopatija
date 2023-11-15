using System.ComponentModel.DataAnnotations.Schema;

namespace homeopatija.Entities;

public class Drug
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public decimal Price { get; set; }
  public int AvailableStock{ get; set; }
  public int OrderedStock { get; set; }
  public List<Drug> CompatibleDrugs { get; set; } // Navigation property for compatibility
}

public class DrugCompatibility
{
    public int Id { get; set; }
    
    public int DrugAId { get; set; }
    public Drug DrugA { get; set; }

    public int DrugBId { get; set; }
    public Drug DrugB { get; set; }
}



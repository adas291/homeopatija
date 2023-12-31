using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homeopatija.Entities;

public class Drug
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Pavadinimas")]
    [Required(ErrorMessage = "Pavadinimas yra privalomas")]
    public string? Title { get; set; }

    [Display(Name = "Aprašas")]
    [Required(ErrorMessage = "Aprašas yra privalomas")]
    public string? Description { get; set; }

    [Display(Name = "Kaina")]
    public decimal Price { get; set; }

    [Display(Name = "Nuotrauka")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Kiekis sandėlyje")]
    public int AvailableStock { get; set; }
    
    [Display(Name = "Užsakytas kiekis")]
    public int OrderedStock { get; set; }
    
    public List<Drug>? CompatibleDrugs { get; set; } // Navigation property for compatibility
}

public class DrugCompatibility
{
    public int Id { get; set; }

    public int DrugAId { get; set; }
    public int DrugBId { get; set; }
}



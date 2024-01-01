using homeopatija.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace homeopatija.Data.Dtos;

public class RegisterUser
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Vardas")]
    [Required(ErrorMessage = "Vardas yra privalomas")]
    public string? Name { get; set; }

    [Display(Name = "Pavardė")]
    [Required(ErrorMessage = "Pavardė yra privaloma")]
    public string? Surname { get; set; }

    [Display(Name = "Slaptažodis")]
    [Required(ErrorMessage = "Slaptažodis yra privalomas")]
    public string? Password { get; set; }

    [Display(Name = "Pakartotas slaptažodis")]
    [Required(ErrorMessage = "Pakartotas slaptažodis yra privalomas")]
    public string? RepeatPassword { get; set; }

    [Display(Name = "Pašto adresas")]
    [Required(ErrorMessage = "Pašto adresas yra privalomas")]
    public string? Email { get; set; }

    [Display(Name = "Telefono numeris")]
    [Required(ErrorMessage = "Telefono numeris yra privalomas")]
    public string? Phone { get; set; }

    [Display(Name = "Adresas")]
    [Required(ErrorMessage = "Adresas yra privalomas")]
    public string? Address { get; set; }
}


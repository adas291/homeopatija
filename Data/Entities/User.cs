using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homeopatija.Entities;

public enum UserType
{
    Client,
    Admin,
    Druggist
}

public class User : IdentityUser<int>
{

    [Display(Name = "Vardas")]
    [Required(ErrorMessage = "Vardas yra privalomas")]
    public string Name { get; set; }

    [Display(Name = "Pavardė")]
    [Required(ErrorMessage = "Pavardė yra privaloma")]
    public string Surname { get; set; }

    //[Display(Name = "Slaptažodis")]
    //[Required(ErrorMessage = "Slaptažodis yra privalomas")]
    //public string Password { get; set; }

    //[Display(Name = "Pašto adresas")]
    //[Required(ErrorMessage = "Pašto adresas yra privalomas")]
    //public string Email { get; set; }

    //[Display(Name = "Telefono numeris")]
    //[Required(ErrorMessage = "Telefono numeris yra privalomas")]
    //public string Phone { get; set; }

    //[Display(Name = "Adresas")]
    //[Required(ErrorMessage = "Adresas yra privalomas")]
    //public string Address { get; set; }

    public UserType UserType { get; set; }

    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //public DateTime CreationDate { get; set; }
}
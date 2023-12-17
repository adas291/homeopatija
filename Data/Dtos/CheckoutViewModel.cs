namespace homeopatija.Data.Dtos;

using System;
using System.ComponentModel.DataAnnotations;

public class CheckoutViewModel
{
    [Required]
    public string NameOnCard { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [CreditCard]
    public string CreditCardNumber { get; set; }

    [Required]
    public string ExpiryDate { get; set; }

    [Required]
    [Range(100, 999)]
    public int CVV { get; set; }
}

using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace homeopatija.Entities;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var password1 = BCrypt.Net.BCrypt.HashPassword("password", salt);

        /*
        modelBuilder.Entity<User>().HasData(
          new User
          {
              Id = 1,
              Name = "DB User1",
              Surname = "Surname1",
              UserType = UserType.Admin,
              //Password = password1,
              Email = "jonas@jontais.lt",
              Phone = "86868",
              Address = "adress1",
              CreationDate = DateTime.Now
          }
        );

        modelBuilder.Entity<Drug>().HasData(
          new Drug
          {
              Id = 1,
              Title = "Procetamolis is db",
              Description = "Labai patikimas is db",
              Price = (decimal)9.99,
              ImageUrl = "~/imgs/drugs/drug1.png",
              OrderedStock = 1,
              AvailableStock = 1,
          }
        );

        modelBuilder.Entity<Comment>().HasData(
          new Comment
          {
              Id = 1,
              Body = "Labai nesknus",
              UserId = 1,
              CreationTime = DateTime.Now,
              DrugId = 1
          }
        );
        */

    }
}
using homeopatija.Entities;
using Microsoft.EntityFrameworkCore;

namespace homeopatija.Repos;

public class UserRepo
{
    public static bool Create(
        HomeopatijaContext db,
        string name,
        string surname,
        string password,
        string email,
        string phone,
        string address,
        UserType userType = UserType.Client)
    {
        db.Users.Add(new User
        {
            Name = name,
            Surname = surname,
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(password),
            Phone = phone,
            UserType = userType,
            Address = address,
            CreationDate = DateTime.Now
        });

        try
        {
            return db.SaveChanges() > 0;
        } catch (DbUpdateException)
        {
            return false;
        }
    }

    public static bool Delete(HomeopatijaContext db, int Id)
    {
        var user = db.Users.Find(Id);
        if (user == null) return false;

        db.Users.Remove(user);
        return db.SaveChanges() > 0;
    }

    public static User? FindByID(HomeopatijaContext db, int id)
    {
        return db.Users.Find(id);
    }
}
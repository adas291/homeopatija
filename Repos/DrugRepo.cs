using homeopatija.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace homeopatija.Repos;

public class DrugRepo
{
    public static string UploadImage(IFormFile image)
    {
        string uploadPath = "imgs/drugs";

        Directory.CreateDirectory("wwwroot/" + uploadPath);
        var imageFileaname = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(image.FileName));
        var imagePath = Path.GetFullPath(Path.Combine("wwwroot/" + uploadPath, imageFileaname));
        var imageUrl = "/" + uploadPath + "/" + imageFileaname;

        using (var stream = File.Create(imagePath))
        {
            image.CopyTo(stream);
        }

        return imageUrl;
    }

    public static void DeleteImage(string imageUrl)
    {
        File.Delete("wwwroot" + imageUrl);
    }

    public static void Create(HomeopatijaContext db, Drug drug)
    {
        db.Drugs.Add(drug);
        db.SaveChanges();
    }

    public static bool Delete(HomeopatijaContext db, int id)
    {
        var result = db.Drugs.Find(id);
        if (result == null) return false;

        if (result.ImageUrl != null)
        {
            DeleteImage(result.ImageUrl);
        }
        db.Drugs.Remove(result);
        db.SaveChanges();
        return true;
    }

    public static Drug? FindByID(HomeopatijaContext db, int id)
    {
        return db.Drugs.Find(id);
    }

    public static bool Edit(HomeopatijaContext db, Drug changes)
    {
        var drug = db.Drugs.Find(changes.Id);
        if (drug == null) return false;
        
        drug.Title = changes.Title;
        if (changes.Description != null)
        {
            drug.Description = changes.Description;
        }
        drug.Price = changes.Price;
        if (changes.ImageUrl != null && changes.ImageUrl != drug.ImageUrl)
        {
            if (drug.ImageUrl != null)
            {
                DeleteImage(drug.ImageUrl);
            }
            drug.ImageUrl = changes.ImageUrl;
        }
        drug.AvailableStock = changes.AvailableStock;
        drug.OrderedStock = changes.OrderedStock;
        try
        {
            db.SaveChanges();
        } catch (DbUpdateException)
        {
            return false;
        }
        return true;
    }

    public static List<Comment> FindCommentsByDrugID(HomeopatijaContext db, int id)
    {
        return db.Comments.Where(x => x.DrugId == id)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.CreationTime)
                            .ToList();
    }

    public static List<Drug> ListAll(HomeopatijaContext db)
    {
        return db.Drugs.ToList();
    }

    public static List<Tuple<int, int>> ListCompatibilities(HomeopatijaContext db)
    {
        var compatibilities = new List<Tuple<int, int>>();
        foreach (var row in db.DrugCompatibilities.ToList())
        {
            compatibilities.Add(Tuple.Create(row.DrugAId, row.DrugBId));
        }
        return compatibilities;
    }

    public static bool AddCompatibility(HomeopatijaContext db, int A, int B)
    {
        int minId = Math.Min(A, B);
        int maxId = Math.Max(A, B);
        var row = new DrugCompatibility{ DrugAId = minId, DrugBId = maxId };
        db.DrugCompatibilities.Add(row);
        try
        {
            return db.SaveChanges() == 1;
        } catch (DbUpdateException)
        {
            return false;
        }
    }

    public static bool RemoveCompatibility(HomeopatijaContext db, int A, int B)
    {
        int minId = Math.Min(A, B);
        int maxId = Math.Max(A, B);
        foreach (var row in db.DrugCompatibilities.Where(row => row.DrugAId == minId && row.DrugBId == maxId))
        {
            if (row == null) continue;
            db.DrugCompatibilities.Remove(row);
        }
        try
        {
            return db.SaveChanges() > 0;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public static bool UpdateCompatibilities(HomeopatijaContext db, List<Tuple<int, int>> newCompatibilities)
    {
        var existingCompatibilities = ListCompatibilities(db);
        foreach (var existingCompatibility in existingCompatibilities)
        {
            if (!newCompatibilities.Contains(existingCompatibility))
            {
                if (!RemoveCompatibility(db, existingCompatibility.Item1, existingCompatibility.Item2))
                {
                    return false;
                }
            }
        }

        foreach (var newCompatibility in newCompatibilities)
        {
            if (!existingCompatibilities.Contains(newCompatibility))
            {
                if (!AddCompatibility(db, newCompatibility.Item1, newCompatibility.Item2))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
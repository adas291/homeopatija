using homeopatija.Entities;
using Microsoft.EntityFrameworkCore;

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

    public static void Delete(HomeopatijaContext db, int id)
    {
        var result = db.Drugs.Find(id);
        if (result == null) return;

        if (result.ImageUrl != null)
        {
            DeleteImage(result.ImageUrl);
        }
        db.Drugs.Remove(result);
        db.SaveChanges();
    }

    public static Drug? FindByID(HomeopatijaContext db, int id)
    {
        return db.Drugs.Find(id);
    }

    public static void Edit(HomeopatijaContext db, Drug changes)
    {
        var drug = db.Drugs.Find(changes.Id);
        if (drug == null) return;
        
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
        db.SaveChanges();
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
}
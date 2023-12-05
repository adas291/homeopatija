namespace homeopatija.Repos;
using homeopatija.Dtos;
using homeopatija.Entities;

public static class FAQRepo
{
    public static int CreateFAQ(HomeopatijaContext db, Faq faq)
    {
        db.Faqs.Add(faq);
        return db.SaveChanges();
    }

    public static int DeleteFAQ(HomeopatijaContext db, int id)
    {
        var result = db.Faqs.Find(id);
        if (result != null)
        {
            db.Faqs.Remove(result);
        }
        return db.SaveChanges();
    }
}
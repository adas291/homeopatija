namespace homeopatija.Repos;
using homeopatija.Data.Dtos;
using homeopatija.Entities;

public class SymptomRepo
{
    public static int EditSymptom(HomeopatijaContext db, Symptom updatedSymptom)
    {
        var existingSymptom = db.Symptoms.Find(updatedSymptom.Id);

        if (existingSymptom != null)
        {
            existingSymptom.Title = updatedSymptom.Title;
            existingSymptom.Description = updatedSymptom.Description;
            return db.SaveChanges();
        }
        return 0;
    }

    public static int CreateSymptom(HomeopatijaContext db, Symptom symptom)
    {
        db.Symptoms.Add(symptom);
        return db.SaveChanges();
    }

    public static int Delete(HomeopatijaContext db, int id)
    {
        var result = db.Symptoms.Find(id);
        if (result != null)
        {
            db.Symptoms.Remove(result);
        }
        return db.SaveChanges();
    }

}
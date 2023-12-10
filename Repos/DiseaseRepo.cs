namespace homeopatija.Repos;
using homeopatija.Dtos;
using homeopatija.Entities;

public class DiseaseRepo
{
    public static int EditDisease(HomeopatijaContext db, Disease updatedDisease)
    {
        var existingDisease = db.Diseases.Find(updatedDisease.Id);

        if (existingDisease != null)
        {
            existingDisease.Name = existingDisease.Name;
            existingDisease.Description = updatedDisease.Description;
            existingDisease.Causes = updatedDisease.Causes;
            existingDisease.Treatment = updatedDisease.Treatment;
            return db.SaveChanges();
        }
        return 0;
    }

    public static int CreateDisease(HomeopatijaContext db, Disease disease)
    {
        db.Diseases.Add(disease);
        return db.SaveChanges();
    }

    public static int Delete(HomeopatijaContext db, int id)
    {
        var result = db.Diseases.Find(id);
        if (result != null)
        {
            db.Diseases.Remove(result);
        }
        return db.SaveChanges();
    }

    public static int CreateDiagnosis(HomeopatijaContext db, Diagnos diagnos)
    {
        db.Diagnosis.Add(diagnos);
        return db.SaveChanges();
    }

}
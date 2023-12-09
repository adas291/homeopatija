namespace homeopatija.Repos;
using homeopatija.Dtos;
using homeopatija.Entities;

public static class BurejaRepo
{
    public static int CreateDiagnosisSymptom(HomeopatijaContext db, DiagnosisSymptom ds)
    {
        db.DiagnosisSymptoms.Add(ds);
        return db.SaveChanges();
    }

    public static int CreateNewDiagnosisSymptom(HomeopatijaContext db, int diagId)
    {
        DiagnosisSymptom ds = new DiagnosisSymptom()
        {
            DiagnosisId = diagId,
            SymptomId = -1,
            Severity = 0
        };
        db.DiagnosisSymptoms.Add(ds);
        return db.SaveChanges();
    }

    public static int RemoveNegativeDiagnosisSymptom(HomeopatijaContext db)
    {
        var oldDiagSymptoms = db.DiagnosisSymptoms.Where(x => x.SymptomId == -1).Select(x => x.Id).ToList();

        var result = db.DiagnosisSymptoms.Find(oldDiagSymptoms);
        if (result != null)
        {
            db.DiagnosisSymptoms.Remove(result);
        }
        return db.SaveChanges();
    }

    public static int CreateNewDiagnosis(HomeopatijaContext db, int userId)
    {
        Diagnos newDiagnos = new Diagnos()
        {
            Certainty = 0,
            Description = "-",
            DiseaseId = -1,
            UserId = userId
        };
        db.Diagnosis.Add(newDiagnos);
        return db.SaveChanges();
    }


}
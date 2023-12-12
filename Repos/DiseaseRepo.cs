namespace homeopatija.Repos;
using homeopatija.Dtos;
using homeopatija.Entities;
using Microsoft.EntityFrameworkCore;

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

    /*public static int CreateDisease(HomeopatijaContext db, Disease disease)
    {
        db.Diseases.Add(disease);
        return db.SaveChanges();
    }*/

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

    public static int CreateDisease(HomeopatijaContext db, Disease disease, List<int> mandatorySymptomIds, List<int> possibleSymptomIds)
    {
        // Add the disease to the database
        db.Diseases.Add(disease);
        db.SaveChanges();

        // Create relationships with mandatory symptoms
        foreach (var symptomId in mandatorySymptomIds)
        {
            var mandatorySymptom = new MandatorDiseaseSymptom(); 
            
            mandatorySymptom.DiseaseId = disease.Id;
            mandatorySymptom.Disease = disease;
            mandatorySymptom.SymptomId  = symptomId;
            mandatorySymptom.Symptom = db.Symptoms.SingleOrDefault(s => s.Id == symptomId);

            db.MandatorDiseaseSymptoms.Add(mandatorySymptom);
        }

        foreach (var symptomId in possibleSymptomIds)
        {
            var possibleSymptom = new PossibleDiseaseSymptom();

            possibleSymptom.DiseaseId = disease.Id;
            possibleSymptom.Disease = disease;
            possibleSymptom.SymptomId = symptomId;
            possibleSymptom.Symptom = db.Symptoms.SingleOrDefault(s => s.Id == symptomId);

            db.PossibleDiseaseSymptoms.Add(possibleSymptom);
        }

        // Save changes to the database
        return db.SaveChanges();
    }

}

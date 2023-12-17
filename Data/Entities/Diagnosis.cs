namespace homeopatija.Entities;

public class Diagnosis
{
    public int Id { get; set; }
    public float Certainty { get; set; }
    public string Description { get; set; }
    public int DiseaseId { get; set; }
    public Disease Disease { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

public class DiagnosisSymptom
{
    public int Id { get; set; }
    public int DiagnosisId { get; set; }
    public Diagnosis Diagnosis { get; set; }
    public int SymptomId { get; set; }
    public Symptom Symptom { get; set; }
    public int Severity { get; set; }
}

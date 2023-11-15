namespace homeopatija.Entities;

public class Disease
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Causes { get; set; }
  public string Treatment { get; set; }
}


public class PossibleDiseaseSymptom
{
  public int Id { get; set; }
  public int DiseaseId { get; set; }
  public Disease Disease{ get; set; }
  public int SymptomId { get; set; }
  public Symptom Symptom { get; set; }
}

public class MandatorDiseaseSymptom
{
  public int Id { get; set; }
  public int DiseaseId { get; set; }
  public Disease Disease{ get; set; }
  public int SymptomId { get; set; }
  public Symptom Symptom { get; set; }
}
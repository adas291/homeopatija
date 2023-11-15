using System.Runtime.ConstrainedExecution;

namespace homeopatija.Entities;


public class Questionnaire
{
  public int Id{ get; set; }
  public byte Intensity { get; set; }
  public int SymptomId { get; set; }
  public Symptom Symptom { get; set; }
  public int UserId{ get; set; }
  public User User { get; set; }
}
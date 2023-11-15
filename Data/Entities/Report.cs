using System.Drawing.Imaging;
using System.Reflection;

namespace homeopatija.Entities;


public enum ReportType
{
  Advertisement,
  Swear,
  Virus,
  Slnder
}
public class Report
{
  public int Id { get; set; }
  public ReportType ReportType{ get; set; }
  public DateTime Date { get; set; }
  public bool IsReviewed { get; set; }
  public int UserId { get; set; }
  public User User{ get; set; }
}
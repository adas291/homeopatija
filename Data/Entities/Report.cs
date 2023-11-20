using System.Drawing.Imaging;
using System.Reflection;
using NuGet.Common;

namespace homeopatija.Entities;


public enum ReportType
{
  Reklama,
  Keiksmažodžiai,
  Virusas,
  Šmeižtas
}



public class Report
{
  public int Id { get; set; }
  public ReportType ReportType{ get; set; }
  public DateTime Date { get; set; } = DateTime.Now;
  public bool IsReviewed { get; set; }

  public int CommentId{ get; set; }
  public Comment Comment{ get; set; }
  public int UserId { get; set; }
  public User User{ get; set; }
}
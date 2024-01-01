namespace homeopatija.Repos;
using homeopatija.Data.Dtos;
using homeopatija.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

public class CommentRepo
{
  private readonly EmailRepo _email;
  private readonly IConfiguration _config;
  private readonly HomeopatijaContext _dbc;
  public CommentRepo(EmailRepo email, IConfiguration config, HomeopatijaContext dbc)
  {
    _email = email;
    _config = config;
    _dbc = dbc;
  }

  public int UpdateComment(Comment updatedComment)
  {
    var existingComment = _dbc.Comments.Find(updatedComment.Id);

    if (existingComment != null)
    {
      existingComment.Body = updatedComment.Body;
      existingComment.CreationTime = updatedComment.CreationTime;
      return _dbc.SaveChanges();
    }
    return 0;
  }

  public async Task<bool> CreateReport(Report report)
  {
    _dbc.Reports.Add(report);
    await _dbc.SaveChangesAsync();

    var newReport = await _dbc.Reports
        .Include(r => r.Comment) 
        .Include(r => r.User)    
        .Include(r => r.Comment.Drug)
        .FirstOrDefaultAsync(r => r.Id == report.Id);

    if (newReport != null)
    {
      string body = @$"Vartotojas, {report.Comment.User.Name} pranešė apie netinkamą komentarą. Priežastis: {Enum.GetName(report.ReportType)} Produktas kurio puslapyje yra komentaras: {report.Comment.Drug?.Title}";
      var result = _email.SendEmail(_config["Email:AdminAddress"], "Netinkamo komentaro pranešimas", body);
      return true;
    }

    return false;
  }


  public int CreateComment(Comment comment)
  {
    comment.Drug = null;
    Console.WriteLine(comment.Drug is not null);
    _dbc.Comments.Add(comment);
    return _dbc.SaveChanges();
  }

  public int Delete(int id)
  {
    var result = _dbc.Comments.Find(id);
    if (result != null)
    {
      _dbc.Comments.Remove(result);
    }
    return _dbc.SaveChanges();
  }
}
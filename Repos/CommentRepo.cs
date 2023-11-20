namespace homeopatija.Repos;
using homeopatija.Dtos;
using homeopatija.Entities;

public static class CommentRepo
{
  public static int UpdateComment(HomeopatijaContext db, Comment updatedComment)
  {
    var existingComment = db.Comments.Find(updatedComment.Id);

    if (existingComment != null)
    {
      existingComment.Body = updatedComment.Body;
      existingComment.CreationTime = updatedComment.CreationTime;
      return db.SaveChanges();
    }
    return 0;
  }

  public static int CreateReport(HomeopatijaContext db, Report report)
  {
    db.Reports.Add(report);
    return db.SaveChanges();
  }

  public static int CreateComment(HomeopatijaContext db, Comment comment)
  {
    comment.Drug = null;
    Console.WriteLine(comment.Drug is not null);
    db.Comments.Add(comment);
    return db.SaveChanges();
  }

  public static int Delete(HomeopatijaContext db, int id)
  {
    var result = db.Comments.Find(id);
    if (result != null)
    {
      db.Comments.Remove(result);
    }
    return db.SaveChanges();
  }



}
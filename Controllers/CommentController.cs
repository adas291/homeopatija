using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Entities;
using homeopatija.Repos;


[Route("Comment")]
public class CommentsController : Controller
{
  private readonly HomeopatijaContext _db;
  public CommentsController(HomeopatijaContext db)
  {
    _db = db;
  }

  [HttpPost("Update")]
  public IActionResult Update(Comment comment)
  {
    //to be fixed when authentication implemented
    comment.UserId = 1;

    var result = CommentRepo.UpdateComment(_db, comment);
    if(result == 1)
    {
      TempData["StatusMessage"] = "Komentaras atnaujintas sėkmingai";
      return RedirectToAction("GetProductView", "Drug", new { id = comment.DrugId});
    }
    else {
      TempData["StatusMessage"] = "Komentaras nebuvo sukurtas";
      return BadRequest();
    }

  }
  [HttpPost("Create")]
  public IActionResult Create(Comment comment)
  {
    var result = CommentRepo.CreateComment(_db, comment);
    if(result == 1)
    {
      TempData["StatusMessage"] = "Komentaras sukurtas sėkmingai";
      // return Redirect($"/drug/{comment.DrugId}");
      return RedirectToAction("GetProductView", "Drug", new { id = comment.DrugId});
      // return Ok();
    }
    else {
      TempData["StatusMessage"] = "Komentaras nebuvo sukurtas";
      return BadRequest();
    }

  }

  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    var result = CommentRepo.Delete(_db, id);
    if(result == 1)
    {
      TempData["StatusMessage"] = "Komentaras pašalintas sėkmingai";
      return Ok(TempData["StatusMessage"]);
    }
    else {
      TempData["StatusMessage"] = "Komentaras nebuvo pašalintas";
      return BadRequest();
    }
  }

  [HttpPost("Report")]
  public IActionResult Report(Report report)
  {
    report.User = null;
    report.UserId = 1;
    var result = CommentRepo.CreateReport(_db, report);
    if(result == 1)
    {
      // TempData["StatusMessage"] = "Pranešimas išsiųstas administratoriui";
      // return RedirectToAction("GetProductView", "Drug", new { id = report.CommentId});
      return Ok();
    }
    else {
      // TempData["StatusMessage"] = "Įvyko klaida";
      // return RedirectToAction("GetProductView", "Drug", new { id = report.CommentId});
      return BadRequest();
    }
  }
}
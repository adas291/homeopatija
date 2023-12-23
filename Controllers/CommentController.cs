using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Entities;
using homeopatija.Repos;


[Route("Comments")]
public class CommentsController : Controller
{
    private readonly HomeopatijaContext _db;
    private readonly CommentRepo _commentRepo;
    public CommentsController(HomeopatijaContext db, CommentRepo commentRepo)
    {
        _db = db;
        _commentRepo = commentRepo;
    }

    [HttpPost("Update")]
    public IActionResult Update(Comment comment)
    {
        //to be fixed when authentication implemented
        comment.UserId = 1;

        var result = _commentRepo.UpdateComment(comment);

        if (result == 1)
        {
            TempData["StatusMessage"] = "Komentaras atnaujintas sėkmingai";
            return RedirectToAction("GetProductView", "Drug", new { id = comment.DrugId });
        }
        else
        {
            TempData["StatusMessage"] = "Komentaras nebuvo sukurtas";
            return RedirectToAction("GetProductView", "Drug", new { id = comment.DrugId });
        }

    }
    [HttpPost("Create")]
    public IActionResult Create(Comment comment)
    {
        var result = _commentRepo.CreateComment(comment);
        if (result == 1)
        {
            TempData["StatusMessage"] = "Komentaras sukurtas sėkmingai";
            return RedirectToAction("GetProductView", "Drug", new { id = comment.DrugId });
        }
        else
        {
            TempData["StatusMessage"] = "Komentaras nebuvo sukurtas";
            return RedirectToAction("GetProductView", "Drug", new { id = comment.DrugId });

            //return BadRequest();
        }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _commentRepo.Delete(id);
        if (result == 1)
        {
            TempData["StatusMessage"] = "Komentaras pašalintas sėkmingai";
            return Ok(TempData["StatusMessage"]);
        }
        else
        {
            TempData["StatusMessage"] = "Komentaras nebuvo pašalintas";
            return BadRequest();
        }
    }

    [HttpPost("Report")]
    public async Task<IActionResult> Report(Report report)
    {
        report.User = null;

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            report.UserId = Convert.ToInt32(userId);
        }
        else
        {
            report.UserId = 1;
        }

        var created = await _commentRepo.CreateReport(report);

        if (created)
        {
            // TempData["StatusMessage"] = "Pranešimas išsiųstas administratoriui";
            // return RedirectToAction("GetProductView", "Drug", new { id = report.CommentId});
            return Ok();
        }
        else
        {
            // TempData["StatusMessage"] = "Įvyko klaida";
            // return RedirectToAction("GetProductView", "Drug", new { id = report.CommentId});
            return BadRequest();
        }
    }
}
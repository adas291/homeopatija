using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;
using System.Xml.Linq;
using homeopatija.Entities;
using homeopatija.Repos;

namespace homeopatija.Controllers;

[Route("FAQ")]
public class FAQController : Controller
{
    private readonly HomeopatijaContext _db;
    public FAQController(HomeopatijaContext db)
    {
        _db = db;
    }
    
    [Route("Create")]
    public IActionResult Create()
    {
        return View("Create");
    }

    [HttpPost("CreateFAQ")]
    public IActionResult CreateFAQ(Faq faq)
    {
        if (faq.Answer is null)
        {
            return BadRequest("Klaida, Answer is null");
        }
        var result = FAQRepo.CreateFAQ(_db, faq);
        if (result != 1)
        {
            TempData["StatusMessage"] = "Komentaras nebuvo sukurtas";
            return BadRequest("Klaida sukuriant DUK");
        }

        TempData["StatusMessage"] = "Komentaras sukurtas sėkmingai";
        return View("Table", _db.Faqs.ToList());

    }

    [Route("Table")]
    public IActionResult Index()
    {
        return View("Table", _db.Faqs.ToList());
    }
    

    [Route("Delete")]
    public IActionResult Delete(int id)
    {
        var result = FAQRepo.DeleteFAQ(_db, id);
        if (result != 1)
        {
            TempData["StatusMessage"] = "DUK nebuvo pašalintas";
            return BadRequest("Klaida ištrinant DUK");
        }

        TempData["StatusMessage"] = "DUK pašalintas sėkmingai";
        return View("Table", _db.Faqs.ToList());
    }
}

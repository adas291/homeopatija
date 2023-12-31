using homeopatija.Data.Dtos;
using homeopatija.Entities;
using homeopatija.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace homeopatija.Controllers;

public class DrugController : Controller
{
    private readonly HomeopatijaContext db;
    private readonly IWebHostEnvironment env;

    public DrugController(HomeopatijaContext db, IWebHostEnvironment env)
    {
        this.db = db;
        this.env = env;
    }

    public void ShowStatusMessage(string text)
    {
        TempData["StatusMessage"] = text;
    }

    [Route("/Drug/Details/{id}")]
    public IActionResult GetProductView(int id)
    {
        var drug = DrugRepo.FindByID(db, id);
        if (drug == null) return Redirect("/404");

        var model = new DrugViewDto
        {
            Drug = drug,
            Comments = DrugRepo.FindCommentsByDrugID(db, id)
        };

        return View("Details", model);
    }

    [Route("Drug")]
    public IActionResult GetDrugTableView()
    {
        return View("Index", DrugRepo.ListAll(db));
    }

    [Route("Drug/Create")]
    public IActionResult CreateDrugView()
    {
        return View("Create");
    }

    [HttpPost("Drug/Create")]
    public IActionResult CreateDrug(Drug drug)
    {
        if (Request.Form.Files.Count != 1)
        {
            ModelState.AddModelError("ImageUrl", "Nuotrauka yra privalomas");
            return View("Create");
        }

        var image = Request.Form.Files.ToList()[0];
        
        drug.ImageUrl = DrugRepo.UploadImage(image);
        DrugRepo.Create(db, drug);
        ShowStatusMessage("Sėkmingai išsaugota");

        return Redirect("/Drug");
    }

    [Route("Drug/Edit/{id}")]
    public IActionResult EditDrugView(int id)
    {
        var drug = DrugRepo.FindByID(db, id);
        if (drug == null) return Redirect("/404");

        return View("Edit", drug);
    }

    [HttpPost("Drug/Edit")]
    public IActionResult EditDrug(Drug drug)
    {
        if (Request.Form.Files.Count == 1)
        {
            var image = Request.Form.Files.ToList()[0];
            drug.ImageUrl = DrugRepo.UploadImage(image);
        }

        bool success = DrugRepo.Edit(db, drug);
        if (success) {
            ShowStatusMessage("Sėkmingai išsaugota");
        } else {
            ShowStatusMessage("Nepavyko išsaugoti");
        }

        return Redirect("/Drug");
    }

    [Route("Drug/Delete/{id}")]
    public IActionResult DeleteDrugView(int id)
    {
        var drug = DrugRepo.FindByID(db, id);
        if (drug == null) return Redirect("/404");

        return View("Delete", drug);
    }

    [HttpPost("Drug/Delete")]
    public IActionResult DeleteDrug()
    {
        int id = int.Parse(Request.Form["Id"]);

        bool success = DrugRepo.Delete(db, id);
        if (success) {
            ShowStatusMessage("Sėkmingai ištrinta");
        } else {
            ShowStatusMessage("Nepavyko ištrinti");
        }

        return RedirectToPage("/Drug");
    }

    [Route("Drug/Compatability")]
    public IActionResult CompatabilityShow()
    {
        return View("CompatabilityMatrix", new
        {
            drugs = DrugRepo.ListAll(db),
            compatibilities = DrugRepo.ListCompatibilities(db)
        });
    }

    [HttpPost("Drug/SaveCompatability")]
    public IActionResult SaveCompatability()
    {
        var compatibilities = new List<Tuple<int, int>>();
        foreach (var key in Request.Form.Keys)
        {
            if (key.StartsWith("__")) continue;

            var parts = key.Split("-");
            Debug.Assert(parts.Length == 2);
            int idA = int.Parse(parts[0]);
            int idB = int.Parse(parts[1]);
            compatibilities.Add(Tuple.Create(idA, idB));
        }

        bool success = DrugRepo.UpdateCompatibilities(db, compatibilities);
        if (success) {
            ShowStatusMessage("Sėkmingai išsaugota");
        } else {
            ShowStatusMessage("Nepavyko išsaugoti");
        }

        return Redirect("/Drug");
    }
}

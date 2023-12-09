using homeopatija.Dtos;
using homeopatija.Entities;
using homeopatija.Repos;
using Microsoft.AspNetCore.Mvc;

namespace homeopatija.Controllers;

public class DrugController : Controller
{
    private readonly HomeopatijaContext db;

    public DrugController(HomeopatijaContext db)
    {
        this.db = db;
    }

    [Route("/Drug/{id}")]
    public IActionResult GetProductView(int id)
    {
        var drug = DrugRepo.FindByID(db, id);
        if (drug == null) return Redirect("/404");

        var model = new DrugViewDto();
        model.Drug = drug;
        model.Comments = DrugRepo.FindCommentsByDrugID(db, id);

        return View("Product", model);
    }

    public IActionResult Index()
    {
        return View(DrugRepo.ListAll(db));
    }

    [Route("Drug/Table")]
    public IActionResult GetDrugTableView()
    {
        return View("Table", DrugRepo.ListAll(db));
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
            return BadRequest();
        }

        var image = Request.Form.Files.ToList()[0];
        
        drug.ImageUrl = DrugRepo.UploadImage(image);
        DrugRepo.Create(db, drug);

        return Redirect("Table");
    }

    [Route("Drug/Details/{id}")]
    public IActionResult DetailsDrug(int id)
    {
        Drug? drug = DrugRepo.FindByID(db, id);
        if (drug == null) return Redirect("/404");

        return View("Details", drug);
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

        Console.WriteLine(drug.Id);
        DrugRepo.Edit(db, drug);

        return Redirect("Table");
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

        DrugRepo.Delete(db, id);
        return Redirect("Table");
    }
}

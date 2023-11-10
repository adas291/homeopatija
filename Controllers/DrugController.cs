using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;

namespace homeopatija.Controllers;

public class DrugController : Controller
{
    public List<Drug> DrugsTable;

    public DrugController()
    {
        DrugsTable = new List<Drug>();

        Random rand = new Random();
        for (int i = 1; i <= 30; i++)
        {
            DrugsTable.Add(new() {
                ID = i,
                Title = $"Procentamolis{i}",
                Price = ((float)rand.NextDouble())*20.0f + 0.99f,
                ImgUrl = "/imgs/drugs/drug1.png"
            });
        }
    }

    public List<Comment> comments = new List<Comment>()
    {
        new (){Id = 1, Body = "baisiai geras", Author = "Aldona0"},
        new (){Id = 2, Body = "baisiai geras", Author = "Aldona1"},
        new (){Id = 3, Body = "baisiai geras", Author = "Aldona2"},
        new (){Id = 4, Body = "baisiai geras", Author = "Aldona3"},
        new (){Id = 5, Body = "baisiai geras", Author = "Aldona4"},
        new (){Id = 6, Body = "baisiai geras", Author = "Aldona5"},
    };

    public Drug? FindDrugByID(int id)
    {
        return DrugsTable.Find(drug => drug.ID == id);
    }

    public IActionResult Index()
    {
        return View(DrugsTable);
    }

    [Route("[controller]/{id}")]
    public IActionResult GetProductView(int id)
    {
        Drug? drug = FindDrugByID(id);
        Debug.Assert(drug != null);

        var model = new DrugView() { Drug = drug, Comments = comments };

        return View("Product", model);
    }

    [Route("[controller]/Table")]
    public IActionResult GetDrugTableView()
    {
        return View("Table", DrugsTable);
    }

    [Route("[controller]/Create")]
    public IActionResult CreateDrug()
    {
        return View("Create");
    }

    [Route("[controller]/Compatability")]
    public IActionResult ShowCompatabilityMatrix()
    {
        return View("CompatabilityMatrix", DrugsTable);
    }

    [Route("[controller]/Details/{id}")]
    public IActionResult DetailsDrug(int id)
    {
        Drug? drug = FindDrugByID(id);
        Debug.Assert(drug != null);

        return View("Details", drug);
    }

    [Route("[controller]/Edit/{id}")]
    public IActionResult EditDrug(int id)
    {
        Drug? drug = FindDrugByID(id);
        Debug.Assert(drug != null);

        return View("Edit", drug);
    }

    [Route("[controller]/Delete/{id}")]
    public IActionResult DeleteDrug(int id)
    {
        Drug? drug = FindDrugByID(id);
        Debug.Assert(drug != null);

        return View("Delete", drug);
    }
}

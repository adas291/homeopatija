using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using AutoMapper;
using homeopatija.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace homeopatija.Controllers;

public class DrugController : Controller
{
  // public List<Comment> comments = new List<Comment>()
  //   {
  //       new (){Id = 1, Body = "baisiai geras", Author = "Aldona0"},
  //       new (){Id = 2, Body = "baisiai geras", Author = "Aldona1"},
  //       new (){Id = 3, Body = "baisiai geras", Author = "Aldona2"},
  //       new (){Id = 4, Body = "baisiai geras", Author = "Aldona3"},
  //       new (){Id = 5, Body = "baisiai geras", Author = "Aldona4"},
  //       new (){Id = 6, Body = "baisiai geras", Author = "Aldona5"},
  //   };
  private readonly HomeopatijaContext _db;
  private readonly IMapper _mapper;

  public DrugController(HomeopatijaContext db, IMapper mapper)
  {
    _db = db;
    _mapper = mapper;
  }


  public IActionResult Index()
  {
    var drugs = _db.Drugs.ToList();

    return View(drugs);
  }
  
  // public Drug? FindDrugByID(int id)
  // {
  //   return DrugsTable.Find(drug => drug.ID == id);
  // }


  [Route("[controller]/{id}")]
  public IActionResult GetProductView(int id)
  {
    

    var model = new DrugViewDto();

    //this code should be in repo todo...
    var drug = _db.Drugs.FirstOrDefault(x => x.Id == id);
    if(drug == null)
    {
      return BadRequest("tokio vaisto nera :/");
    }

    model.Drug = drug;
    model.Comments = _db.Comments.Include(x=>x.User).Where(x => x.DrugId == id).ToList();

    return View("Product", model);
  }

  [Route("[controller]/Table")]
  public IActionResult GetDrugTableView()
  {
    return View("Table", _db.Drugs.ToList());
  }

  // [Route("[controller]/Create")]
  // public IActionResult CreateDrug()
  // {
  //   return View("Create");
  // }

  // [Route("[controller]/Compatability")]
  // public IActionResult ShowCompatabilityMatrix()
  // {
  //   return View("CompatabilityMatrix", DrugsTable);
  // }

  // [Route("[controller]/Details/{id}")]
  // public IActionResult DetailsDrug(int id)
  // {
  //   Drug? drug = FindDrugByID(id);
  //   Debug.Assert(drug != null);

  //   return View("Details", drug);
  // }

  // [Route("[controller]/Edit/{id}")]
  // public IActionResult EditDrug(int id)
  // {
  //   Drug? drug = FindDrugByID(id);
  //   Debug.Assert(drug != null);

  //   return View("Edit", drug);
  // }

  // [Route("[controller]/Delete/{id}")]
  // public IActionResult DeleteDrug(int id)
  // {
  //   Drug? drug = FindDrugByID(id);
  //   Debug.Assert(drug != null);

  //   return View("Delete", drug);
  // }
}

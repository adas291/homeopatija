using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;

namespace homeopatija.Controllers;

public class DrugController : Controller
{

    public List<Drug> DrugsTable = new List<Drug>()
    {
      new (){ID = 1, Title = "Procentamolis1", Price = (float)5.99, ImgUrl = "/imgs/drugs/drug1.png"},
      new (){ID = 2, Title = "Procentamolis2", Price = (float)5.99, ImgUrl = "/imgs/drugs/drug1.png"},
      new (){ID = 3, Title = "Procentamolis3", Price = (float)5.99, ImgUrl = "/imgs/drugs/drug1.png"},
      new (){ID = 4, Title = "Procentamolis4", Price = (float)5.99, ImgUrl = "/imgs/drugs/drug1.png"},
      new (){ID = 5, Title = "Procentamolis5", Price = (float)5.99, ImgUrl = "/imgs/drugs/drug1.png"}
    };

    public List<Comment> comments = new List<Comment>()
    {
      new (){Id = 2, Body = "baisiai geras", Author = "Aldona1"},
      new (){Id = 3, Body = "baisiai geras", Author = "Aldona2"},
      new (){Id = 4, Body = "baisiai geras", Author = "Aldona3"},
      new (){Id = 5, Body = "baisiai geras", Author = "Aldona4"},
      new (){Id = 6, Body = "baisiai geras", Author = "Aldona5"},
  };

  public IActionResult Index()
  {
    return View(DrugsTable);
  }


  [Route("[controller]/{id}")]
  public IActionResult GetProductView(int id)
  {
    //simulated database
    var model = new DrugView() { Drug = DrugsTable[id], Comments = comments };

    return View("Product", model);
  }


}

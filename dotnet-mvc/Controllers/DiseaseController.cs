using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;

namespace homeopatija.Controllers;

public class DiseaseController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}

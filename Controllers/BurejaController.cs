using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;

namespace homeopatija.Controllers;

public class BurejaController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("[controller]/Klausimynas")]
    public IActionResult Klausimynas()
    {
        return View("Klausimynas");
    }

    public IActionResult OnPostButton1(IFormCollection data)
    {
        return View();
    }


    public IActionResult OnPostButton2(IFormCollection data)
    {
        return View();
    }
}

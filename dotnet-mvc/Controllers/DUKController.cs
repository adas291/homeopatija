using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;
using System.Xml.Linq;

namespace homeopatija.Controllers;

public class DUKController : Controller
{

    public List<DUK> DUKTable = new List<DUK>()
    {
        new DUK(){klausimas = "kodėl taip vyksta", atsakymas = "nes galūnė ėl.", sukurimo_data = new DateTime(2023, 11, 5), eiliskumas = 1},
        new DUK(){klausimas = "ar aš sveiko proto", atsakymas = "nueik pas Nadėždą prie stoties.", sukurimo_data = new DateTime(2001, 9, 1), eiliskumas = 2}
    };

    public IActionResult Index()
    {
        return View("Table", DUKTable);
    }

    [Route("[controller]/Create")]
    public IActionResult Create()
    {
        return View("Create");
    }
}

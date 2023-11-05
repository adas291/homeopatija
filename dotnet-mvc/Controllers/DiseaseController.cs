using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;
using System.Xml.Linq;

namespace homeopatija.Controllers;

public class DiseaseController : Controller
{

	public List<Disease> DiseaseTable = new List<Disease>()
	{
	  new (){Title = "Bloga Liga", Cause = "Buvimas nesveiku", Description = "nefaina liga", Treatment = "Pasveikimas"},

	};

	public IActionResult Index()
	{
		return View(DiseaseTable);
	}
	//[Route("[controller]/{id}")]
	public IActionResult GetProductView(int id)
	{
		//simulated database
		var model = new DiseaseView() { Disease = DiseaseTable[id] };

		return View("Product", model);
	}
    public Disease? FindDiseaseByID(int id)
    {
        return DiseaseTable.Find(drug => drug.id == id);
    }


    [Route("[controller]/Table")]
    public IActionResult GetDiseaseTableView()
    {
        return View("Table", DiseaseTable);
    }

    [Route("[controller]/Create")]
    public IActionResult CreateDrug()
    {
        return View("Create");
    }

    [Route("[controller]/Compatability")]
    public IActionResult ShowCompatabilityMatrix()
    {
        return View("CompatabilityMatrix", DiseaseTable);
    }

    [Route("[controller]/Details/{id}")]
    public IActionResult DetailsDisease(int id)
    {
        Disease? Disease = FindDiseaseByID(id);
        Debug.Assert(Disease != null);

        return View("Details", Disease);
    }

    [Route("[controller]/Edit/{id}")]
    public IActionResult EditDisease(int id)
    {
        Disease? Disease = FindDiseaseByID(id);
        Debug.Assert(Disease != null);

        return View("Edit", Disease);
    }

    [Route("[controller]/Delete/{id}")]
    public IActionResult DeleteDrug(int id)
    {
        Disease? Disease = FindDiseaseByID(id);
        Debug.Assert(Disease != null);

        return View("Delete", Disease);
    }
}

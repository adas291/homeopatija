using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Entities;
using homeopatija.Repos;
using AutoMapper;
using homeopatija.Dtos;
using Microsoft.EntityFrameworkCore;
using homeopatija.Models;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace homeopatija.Controllers;

[Route("Symptom")]

public class SymptomController : Controller
{
    private readonly HomeopatijaContext _db;

    public SymptomController(HomeopatijaContext db)
    {
        _db = db;
    }

    [HttpPost("EditSymptom")]
    public IActionResult EditSymptom(Entities.Symptom symptom)
    {
        if (ModelState.IsValid)
        {
            var result = SymptomRepo.EditSymptom(_db, symptom);

            if (result == 1)
            {
                TempData["StatusMessage"] = "simptomas atnaujintas sėkmingai";
                return RedirectToAction("Table");
            }
        }

        TempData["StatusMessage"] = "simptoams nebuvo atnaujintas";
        return View("Edit", symptom);
    }

    [Route("Edit")]
    public IActionResult Edit(int id)
    {
        var symptom = _db.Symptoms.FirstOrDefault(d => d.Id == id);

        if (symptom == null)
        {
            return NotFound(); //reiks pakeist veliau
        }

        return View("Edit", symptom); // bsk blogai jc ,nes nerasem
    }

    [Route("Create")]

    public IActionResult Create()
    {
        return View("Create");

    }

    [HttpPost("CreateSymptom")]

    public IActionResult CreateSymptom(Entities.Symptom symptom)
    {
        if (symptom.Title is null)
        {
            return BadRequest("Klaida, Title is null");
        }
        if (symptom.Description is null)
        {
            return BadRequest("Klaida, Description is null");
        }

        var result = SymptomRepo.CreateSymptom(_db, symptom);
        if (result != 1)
        {
            TempData["StatusMessage"] = "simptomas nebuvo įrašytas";
            return BadRequest("Klaida įrašant ligą");
        }

        TempData["StatusMessage"] = "simptomas įrašyta sėkmingai";
        return View("Table", _db.Symptoms.ToList());
    }

    [Route("Table")]
    public IActionResult Index()
    {
        var symptoms = _db.Symptoms.ToList();
        ViewBag.Symptoms = symptoms;
        return View("Table", _db.Symptoms.ToList());
    }

    [Route("Delete")]
    public IActionResult Delete(int id)
    {
        var symptomToDelete = _db.Symptoms.FirstOrDefault(d => d.Id == id);

        if (symptomToDelete == null)
        {
            return NotFound(); // Return a 404 Not Found response or display an error message
        }

        return View("Delete", symptomToDelete);
    }


    [Route("DeleteSymptom")]
    public IActionResult DeleteSymptom(int id)
    {
        var result = SymptomRepo.Delete(_db, id);
        if (result != 1)
        {
            TempData["StatusMessage"] = "simptomas nebuvo pašalintas";
            return BadRequest("Klaida ištrinant ligą");
        }

        TempData["StatusMessage"] = "simptomas pašalintas sėkmingai";
        return View("Table", _db.Symptoms.ToList());
    }
}

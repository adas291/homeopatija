﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;
using System.Security.Cryptography;
using homeopatija.Entities;
using System;
using homeopatija.Repos;
using System.Linq;
using NuGet.Protocol.Events;

namespace homeopatija.Controllers;

public class BurejaController : Controller
{
    private readonly HomeopatijaContext _db;
    public BurejaController(HomeopatijaContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("[controller]/Horoscope")]
    public IActionResult Horoscope()
    {
        return View("Horoscope");//change to get based on current user
    }

    [Route("[controller]/AskQuestion")]
    public IActionResult Answer(string question)
    {
        string[] files = Directory.GetFiles($"wwwroot\\imgs\\tarotCards");
        var random = new Random();
        string selectedImage = files[random.Next(files.Length)];
        var answer = new Entities.Question()
        {
            question = question,
            answer = $"/imgs/tarotCards/{selectedImage.Split('\\').Last()}"
        };

        return View(answer);
    }

    [Route("[controller]/Klausimynas")]
    public IActionResult Klausimynas()
    {
        int userId = 1;//TODO: Change user id to current user

        int diagnosisId = GetDiagnosisId(userId);
        if (diagnosisId == -1)
        {
            TempData["StatusMessage"] = "nepavyko sukurti naujos diagnozės";
            return BadRequest("Klaida sukuriant naują diagnozę");
        }

        Symptom? unasked = GetUnaskedSymptom(diagnosisId);
        if (unasked == null)
        {
            TempData["StatusMessage"] = "nepavyko sukurti naujos diagnozės";
            return BadRequest("Klaida sukuriant naują diagnozę");
        }
        if (unasked.Id == 0) //Kai pasibaigia klausimynas
        {
            Debug.WriteLine("Got enough symptoms");
            return View("Index");
        }

        var model = new Entities.Question()
        {
            question = $"Ar jaučiate šį simptomą:\n{unasked.Title}",
            currentDiagId = diagnosisId,
            currentSymId = unasked.Id
        };

        return View(model);
    }


    public int GetDiagnosisId(int userId)
    {
        var createdBefore = _db.Diagnosis
            .Where(x => x.UserId == userId && x.DiseaseId == -1).ToList();
        if (createdBefore.Count == 0)
        {
            var result = BurejaRepo.CreateNewDiagnosis(_db, userId);
            if (result != 1)
            {
                return -1;
            }

            return _db.Diagnosis
                .Where(x => x.UserId == userId && x.DiseaseId == -1).Last().Id;
        }

        int lastId = createdBefore.Last().Id;
        var associatedDiagSyms = _db.DiagnosisSymptoms
            .Where(x => x.DiagnosisId == lastId && x.SymptomId == -1).ToList();

        if (associatedDiagSyms.Count == 0)
        {
            var result = BurejaRepo.CreateNewDiagnosisSymptom(_db, lastId);
            if (result != 1)
            {
                return -1;
            }
        }
        return lastId;
    }

    public Symptom? GetUnaskedSymptom(int diagnosisId)
    {
        var asked = _db.DiagnosisSymptoms
            .Where(x => x.DiagnosisId == diagnosisId)
            .Select(x => x.SymptomId)
            .Distinct().ToList();

        Debug.WriteLine($"Asked: {string.Join(", ", asked)}");
        var symptomIds = _db.Symptoms.Select(x => x.Id).ToList();
        var unasked = symptomIds.Except(asked).ToList();
        if (asked.Count > 10)
        {
            return new Symptom() { Id = 0 };
        }

        List<(int, double)> idToUsefulness = new List<(int, double)>();
        foreach (int id in unasked)
        {
            int usefullness = _db.MandatorDiseaseSymptoms.Where(x => x.SymptomId == id).Count();
            int half_usefullness = _db.PossibleDiseaseSymptoms.Where(x => x.SymptomId == id).Count();
            idToUsefulness.Add((id, usefullness + half_usefullness / 2.0));
        }
        var selectedSymptom = idToUsefulness.OrderByDescending(x => x.Item2).First();
        return _db.Symptoms.Where(x => x.Id == selectedSymptom.Item1).FirstOrDefault();
    }

    [HttpPost]
    public IActionResult AnswerQuestionaire(Entities.Question ans)
    {
        if (ans.answer == null)
        {
            return RedirectToAction("Klausimynas");
        }
        DiagnosisSymptom ds = new DiagnosisSymptom()
        {
            DiagnosisId = ans.currentDiagId,
            SymptomId = ans.currentSymId,
            Severity = int.Parse(ans.answer)
        };
        var result = BurejaRepo.CreateDiagnosisSymptom(_db, ds);
        if (result != 1)
        {
            TempData["StatusMessage"] = "diagnozės simptomas nebuvo sukurtas";
            return BadRequest("Klaida sukuriant diagnozės simptomą");
        }
        return RedirectToAction("Klausimynas");
    }
}

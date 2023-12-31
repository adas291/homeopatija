﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Entities;
using homeopatija.Repos;
using AutoMapper;
using homeopatija.Data.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace homeopatija.Controllers;

[Route("Disease")]

public class DiseaseController : Controller
{
    private readonly HomeopatijaContext _db;

    public DiseaseController(HomeopatijaContext db)
    {
        _db = db;
    }

    [HttpPost("EditDisease")]
    public IActionResult EditDisease(Entities.Disease disease)
    {
        if (ModelState.IsValid)
        {
            var result = DiseaseRepo.EditDisease(_db, disease);

            if (result == 1)
            {
                TempData["StatusMessage"] = "Liga atnaujinta sėkmingai";
                return RedirectToAction("Table");
            }
        }

        TempData["StatusMessage"] = "Liga nebuvo atnaujinta";
        return View("Edit", disease);
    }

    [Route("Edit")]
    public IActionResult Edit(int id)
    {
        var disease = _db.Diseases.FirstOrDefault(d => d.Id == id);

        if (disease == null)
        {
            return NotFound(); //reiks pakeist veliau
        }

        return View("Edit", disease); // bsk blogai jc ,nes nerasem
    }

    [Route("Create")]

    public IActionResult Create()
    {
        var symptoms = _db.Symptoms.ToList();
        ViewBag.Symptoms = symptoms.Skip(1);
        return View("Create");

    }

    [HttpPost("CreateDisease")]
    public IActionResult CreateDisease(Entities.Disease disease, List<int> mandatorySymptomIds, List<int> possibleSymptomIds)
    {
        if (disease.Name is null || disease.Description is null || disease.Causes is null || disease.Treatment is null)
        {
            return BadRequest("Klaida, vienas arba keli laukai yra tušti.");
        }

        if (mandatorySymptomIds.Count == 0 && possibleSymptomIds.Count == 0)
        {
            return BadRequest("Klaida, nepasirinktas nei vienas simptomas.");
        }
        var result = DiseaseRepo.CreateDisease(_db, disease, mandatorySymptomIds, possibleSymptomIds);

        if (result <= 0)
        {
            TempData["StatusMessage"] = "Liga nebuvo įrašyta";
            return BadRequest("Klaida įrašant ligą");
        }

        TempData["StatusMessage"] = "Liga įrašyta sėkmingai";
        return View("Table", _db.Diseases.ToList());
    }





    [Route("Table")]
    public IActionResult Index()
    {
        var symptoms = _db.Symptoms.ToList();
        ViewBag.Symptoms = symptoms.Skip(1);
        return View("Table", _db.Diseases.ToList());
    }

    [Route("Delete")]
    public IActionResult Delete(int id)
    {
        var diseaseToDelete = _db.Diseases.FirstOrDefault(d => d.Id == id);

        if (diseaseToDelete == null)
        {
            return NotFound(); // Return a 404 Not Found response or display an error message
        }

        return View("Delete", diseaseToDelete);
    }


    [Route("DeleteDisease")]
    public IActionResult DeleteDisease(int id)
    {
        var result = DiseaseRepo.Delete(_db, id);
        if (result <= 0)
        {
            TempData["StatusMessage"] = "Liga nebuvo pašalinta";
            return BadRequest("Klaida ištrinant ligą");
        }

        TempData["StatusMessage"] = "Liga pašalinta sėkmingai";
        return View("Table", _db.Diseases.ToList());
    }

    [Route("Details/{id}")]
    public IActionResult Details(int id)
    {
        var disease = _db.Diseases.FirstOrDefault(d => d.Id == id);

        if (disease == null)
        {
            return NotFound();
        }

        var mandatorySymptomIds = _db.MandatorDiseaseSymptoms
            .Where(mds => mds.DiseaseId == id)
            .Select(mds => mds.SymptomId)
            .ToList();

        var optionalSymptomIds = _db.PossibleDiseaseSymptoms
            .Where(pds => pds.DiseaseId == id)
            .Select(pds => pds.SymptomId)
            .ToList();

        var mandatorySymptoms = _db.Symptoms.Where(s => mandatorySymptomIds.Contains(s.Id)).ToList();
        var optionalSymptoms = _db.Symptoms.Where(s => optionalSymptomIds.Contains(s.Id)).ToList();

        ViewBag.MandatorySymptoms = mandatorySymptoms;
        ViewBag.OptionalSymptoms = optionalSymptoms;

        return View(disease);
    }


    [HttpPost("Table")]
    [HttpPost]
    public IActionResult FilterBySymptoms(List<int> selectedSymptomIds) // denounce linq, embrace traditionalism
    {
        if (selectedSymptomIds == null || !selectedSymptomIds.Any())

        {
            // If no symptoms are selected, return all diseases
            var allDiseases = _db.Diseases.ToList();
            return PartialView("_DiseasesTablePartial", allDiseases.Skip(1));
        }

        List<Entities.Disease> diseasesWithSelectedSymptoms = new List<Entities.Disease>();

        foreach (var disease in _db.Diseases)
        {

            int count = 0;

            foreach (var selectedSymptomId in selectedSymptomIds)
            {
                bool hasMandatorSymptom = _db.MandatorDiseaseSymptoms
                    .Any(mds => mds.DiseaseId == disease.Id && mds.SymptomId == selectedSymptomId);

                bool hasPossibleSymptom = _db.PossibleDiseaseSymptoms
                    .Any(pds => pds.DiseaseId == disease.Id && pds.SymptomId == selectedSymptomId);

                if (hasMandatorSymptom || hasPossibleSymptom)
                {
                    count++;
                }
            }

            if (count == selectedSymptomIds.Count)
            {
                diseasesWithSelectedSymptoms.Add(disease);
            }
        }


        return PartialView("_DiseasesTablePartial", diseasesWithSelectedSymptoms);
    }
    //diagnoze
    [Route("Diagnosis")]
    public async Task<IActionResult> CreateDiagnosis()
    {
        // Assuming userId is 1 for now
        int userId = 1;

        var pendingDiagnosis = await _db.Diagnosis.FirstOrDefaultAsync(d => d.DiseaseId == -1);
        Entities.Disease bestMatchedDisease = null;
        // Fetch all disease
        var diseases = await _db.Diseases
            .ToListAsync();

        if (pendingDiagnosis != null)
        {
            // Find symptoms for the pending diagnosis from DiagnoseSymptoms
            var diagnosisSymptoms = await _db.DiagnosisSymptoms
                .Where(ds => ds.DiagnosisId == pendingDiagnosis.Id)
                .ToListAsync();
            Debug.WriteLine(diagnosisSymptoms.Count());




            float maxSimilarity = 0;

            foreach (var disease in diseases)
            {
                Debug.WriteLine(disease.Name);

                var mandatorySymptoms = await _db.MandatorDiseaseSymptoms
                    .Where(mds => mds.DiseaseId == disease.Id)
                    .Select(mds => mds.SymptomId)
                    .ToListAsync();

                var possibleSymptoms = await _db.PossibleDiseaseSymptoms
                    .Where(pds => pds.DiseaseId == disease.Id)
                    .Select(pds => pds.SymptomId)
                    .ToListAsync();

                // Calculate similarity based on symptoms' severity for the pending diagnosis
                var sharedMandatorySymptoms = diagnosisSymptoms.Count(ds => ds.Severity == 1 && mandatorySymptoms.Contains(ds.SymptomId));
                var sharedPossibleSymptoms = diagnosisSymptoms.Count(ds => ds.Severity == 1 && possibleSymptoms.Contains(ds.SymptomId));

                // Calculate similarity considering weighted mandatory symptoms
                float similarity = (sharedMandatorySymptoms * 2) + sharedPossibleSymptoms;

                // Update if the disease has higher similarity
                if (similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    bestMatchedDisease = disease;
                }
            }

            // Update the existing diagnosis if a matching disease is found
             if (bestMatchedDisease != null)
             {
                 pendingDiagnosis.UserId = userId;
                 pendingDiagnosis.DiseaseId = bestMatchedDisease.Id;
                 pendingDiagnosis.Description = $"{bestMatchedDisease.Description}, {bestMatchedDisease.Causes}, {bestMatchedDisease.Treatment}";
                 pendingDiagnosis.Certainty = maxSimilarity; 

                 await _db.SaveChangesAsync();
             }
        }
        if (bestMatchedDisease == null)
        {
            pendingDiagnosis.UserId = userId;
            pendingDiagnosis.DiseaseId = diseases[1].Id;
            pendingDiagnosis.Description = $"{diseases[1].Description}, {diseases[1].Causes}, {diseases[1].Treatment}";
            pendingDiagnosis.Certainty = 0;
            await _db.SaveChangesAsync();
            TempData["StatusMessage"] = "Trūksta duomenų";
            return RedirectToAction("Index", "Bureja");
        }
        Debug.WriteLine("rasta liga:");
        Debug.WriteLine(bestMatchedDisease.Name);
        return RedirectToAction("Diagnosis", "Bureja");
    }
}

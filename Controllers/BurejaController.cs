using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Models;
using System.Security.Cryptography;
using homeopatija.Entities;
using System;

namespace homeopatija.Controllers;

public class BurejaController : Controller
{
    public int asked = 0;

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
        return View("Horoscope");
    }

    [Route("[controller]/AskQuestion")]
    public IActionResult Answer(string question)
    {
        Debug.WriteLine(question);
        string[] files = Directory.GetFiles($"wwwroot\\imgs\\tarotCards");
        var random = new Random();
        string selectedImage = files[random.Next(files.Length)];
        var answer = new Entities.Question()
        {
            question = $"/imgs/tarotCards/{selectedImage.Split('\\').Last()}"
        };

        return View(answer);
    }

    [Route("[controller]/Klausimynas")]
    public IActionResult Klausimynas()
    {
        var model = new Entities.Question()
        {
            question = "Ar jaučiate galvos skausmą?"
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult AnswerQuestionaire(string question, string answer)
    {
        Debug.WriteLine($"{question} \"{answer}\"");
        return RedirectToAction("Index");
    }
}

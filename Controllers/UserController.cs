using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using homeopatija.Entities;
using homeopatija.Data.Dtos;
using homeopatija.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace homeopatija.Controllers
{
    public class UserController : Controller
    {
        private readonly HomeopatijaContext db;

        public UserController(HomeopatijaContext db)
        {
            this.db = db;
        }

        public void ShowStatusMessage(string text)
        {
            TempData["StatusMessage"] = text;
        }

        [Route("Details")]
        public ActionResult Details(int id)
        {
            return View("Details");
        }

        [Route("Register")]
        public ActionResult Register()
        {
            return View("Register");
        }

        [Route("Login")]
        public ActionResult Login()
        {
            
            return View("Login");
        }

        [HttpPost("Register")]
        public ActionResult Create(RegisterUser userData)
        {
            /*
            if (userData.Password != userData.RepeatPassword)
            {
                ModelState.AddModelError("RepeatPassword", "Pakartotas slaptažodis nesutampa");
                return View("Register");
            }

            Console.WriteLine("foo");
            Console.WriteLine(userData.Password);
            bool success = UserRepo.Create(db, userData.Name, userData.Surname, userData.Password, userData.Email, userData.Phone, userData.Address);
            if (!success)
            {
                ShowStatusMessage("Nepavyko užsiregistruoti");
                return View("Register");
            }

            ShowStatusMessage("Sėkmingai užsiregistruota");
            return RedirectToAction("Login");
            */
            return RedirectToPage("/404");
        }



        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            User user = new User(){
                Name = "Petras",
                Surname = "Dovydaitis",
                //Password = "grietinė69",
                Email = "petras.dovydaitis@vilkaviškiopieninė.lt",
                //Phone = "+370********"
            };

            return View("Edit", user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            return View();
            /*try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }*/
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using homeopatija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace homeopatija.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShoppingCartViewModel GetCart()
        {
            var cart = new ShoppingCartViewModel
            {
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductId = 1, ProductName = "Homeopatinė Arnica Montana", Price = 5.99m, Quantity = 1 },
                    new ShoppingCartItem { ProductId = 2, ProductName = "Homeopatinis Belladonna", Price = 7.49m, Quantity = 2 }
                }
            };

            return cart;
        }

        // GET: ShoppingCartController
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // GET: ShoppingCartController/Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        // GET: ShoppingCartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ShoppingCartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ShoppingCartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCartController/Delete/5
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

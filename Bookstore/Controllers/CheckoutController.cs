using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class CheckoutController : Controller
    {
        private ICheckoutRepository repo { get; set; }
        private Cart cart { get; set; }
        public CheckoutController(ICheckoutRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        // Create Get and Post controller actions for the Checkout page, and the post should return
        // the confirmation page
        [HttpGet]
        public IActionResult CheckoutPg()
        {
            return View(new Purchase());
        }

        [HttpPost]
        public IActionResult CheckoutPg(Purchase purchase)
        {
            if (cart.Books.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your basket is empty");
            }

            if (ModelState.IsValid)
            {
                purchase.Lines = cart.Books.ToArray();
                repo.SavePurchase(purchase);
                cart.ClearCart();

                return RedirectToPage("/PurchaseConfirmation");
            }
            else
            {
                return View();
            }
        }
    }
}

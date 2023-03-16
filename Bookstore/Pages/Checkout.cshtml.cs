using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class CheckoutModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        public CheckoutModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int BookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);


            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddBook(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}

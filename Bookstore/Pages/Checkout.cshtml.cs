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
        // Add functionality that shows the user their cart and remembers what page and filter 
        // they had active when they added that book so that they can return to that exact place 
        // from their cart if they click "Continue Shopping"

        private IBookstoreRepository repo { get; set; }

        public CheckoutModel (IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int BookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);

            cart.AddBook(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        // Return the cart again but without the item that they just removed from their cart
        public IActionResult OnPostRemove(int BookId, string returnUrl)
        {
            cart.RemoveItem(cart.Books.First(x => x.Bk.BookId == BookId).Bk);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}

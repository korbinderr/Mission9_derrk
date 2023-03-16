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
        }

        public IActionResult OnPost(int BookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);

            cart.AddBook(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int BookId, string returnUrl)
        {
            cart.RemoveItem(cart.Books.First(x => x.Bk.BookId == BookId).Bk);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}

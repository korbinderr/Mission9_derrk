using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        // Disjoined now.. 'abstraction'. the controller isn't calling the context anymore, doesn't even know... all it knows is that it's calling some repository
        public HomeController(IBookstoreRepository rep)
        {
            repo = rep;
        }

        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize =10;

            var i = new BooksViewModel
            {
                Books = repo.Books
                .Where(c => c.Category == category || category == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                Pageinfo = new PageInfo
                {
                    TotalNumBooks = (category == null ? repo.Books.Count() : repo.Books.Where(x=>x.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            //var books = repo.Books
            //    .OrderBy(b => b.Title)
            //    .Skip((pageNum-1) * pageSize)
            //    .Take(pageSize);

            return View(i);
        }
    }
}

using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Cart
    {
        public List<CartLineItem> Books { get; set; } = new List<CartLineItem>();

        public void AddBook(Book book, int qty)
        {
            CartLineItem item = Books.Where(b => b.Bk.BookId == book.BookId).FirstOrDefault();

            if (item == null)
            {
                Books.Add(new CartLineItem
                {
                    Bk = book,
                    Quantity = qty
                });
            }

            else
            {
                item.Quantity += qty;
            }
        }

        public double CalculateTotal()
        {
            double sum = Books.Sum(x => (x.Bk.Price * x.Quantity));

            return sum;
        }
    }



    public class CartLineItem
    {
        public int LineID { get; set; }
        public Book Bk{ get; set; }
        public int Quantity { get; set; }
    }
}

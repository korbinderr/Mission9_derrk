using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Cart
    {
        public List<CartLineItem> Books { get; set; } = new List<CartLineItem>();

        // Create method that adds a book to the cart
        public virtual void AddBook(Book book, int qty)
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

        // Add methods to remove a book from the cart list
        public virtual void RemoveItem(Book book)
        {
            Books.RemoveAll(x => x.Bk.BookId == book.BookId);
        }

        // Clear the cart, will be useful in the checkout actions
        public virtual void ClearCart()
        {
            Books.Clear();
        }

        // This creates a total cost for all books X their quantity in the cart
        public double CalculateTotal()
        {
            double sum = Books.Sum(x => (x.Bk.Price * x.Quantity));

            return sum;
        }
    }



    public class CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Bk{ get; set; }
        public int Quantity { get; set; }
    }
}

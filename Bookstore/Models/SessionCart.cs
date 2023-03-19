using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Models
{

    // Create ability to have an active session so that when the user goes between pages,
    // and back and forth between 'browsing' and their 'cart' they don't lose what books have
    // been added to their cart. 
    public class SessionCart : Cart
    {

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionCart cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();

            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddBook(Book book, int qty)
        {
            base.AddBook(book, qty);
            Session.SetJson("cart", this);
        }

        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("cart", this);
        }

        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("cart");
        }
    }
}



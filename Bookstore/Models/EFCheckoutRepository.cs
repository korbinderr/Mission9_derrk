using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private BookstoreContext context;

        public EFCheckoutRepository (BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Purchase> Purchase => context.Purchase.Include(x => x.Lines).ThenInclude(x => x.Bk);

        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Bk));

            if (purchase.PurchaseID == 0)
            {
                context.Purchase.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}

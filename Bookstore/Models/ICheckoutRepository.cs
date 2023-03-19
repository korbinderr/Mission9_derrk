using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public interface ICheckoutRepository
    {
        IQueryable<Purchase> Purchase { get; }

        void SavePurchase(Purchase purchase);
    }
}

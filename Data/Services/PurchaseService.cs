using KhumaloCraftLtd.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace KhumaloCraftLtd.Data.Services
{
    public class PurchaseService : IPurchaseService
    {

        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;
        }
       
            public async Task Add(Purchase purchase)
            {
                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();
            }
        

        public IQueryable<Purchase> GetAll()
        {
            var applicationDbContext = from a in _context.Purchases.Include(l => l.Product).ThenInclude(l => l.User)
                                       select a;
            return applicationDbContext;
          
        }
    }
}

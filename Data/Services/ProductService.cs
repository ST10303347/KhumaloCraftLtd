using KhumaloCraftLtd.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KhumaloCraftLtd.Data.Services
{
    public class ProductService : IProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(ProductModel product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ProductModel> GetAll()
        {
            var applicationDbContext = _context.Products.Include(p => p.User);
            return applicationDbContext;
            
        }

        public async Task<ProductModel> GetById(int? id)
        {
            var product1 = await _context.Products
               .Include(l => l.User)
              
             
               .FirstOrDefaultAsync(m => m.Id == id);
            return product1;
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

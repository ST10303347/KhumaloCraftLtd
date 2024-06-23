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

        public Task<ProductModel> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

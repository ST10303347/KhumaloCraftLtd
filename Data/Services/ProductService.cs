using KhumaloCraftLtd.Models;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraftLtd.Data.Services
{
    public class ProductService : IProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(ProductModel product)
        {
            var applicationDbContext = _context.Products.Include(p => p.User);
            return (Task)applicationDbContext;
        }

        public IQueryable<ProductModel> GetAll()
        {
            throw new NotImplementedException();
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

using KhumaloCraftLtd.Models;
using System.Reflection;

namespace KhumaloCraftLtd.Data.Services
{
    public interface IProductsService
    {
        IQueryable<ProductModel> GetAll();
        Task Add(ProductModel product);
        Task<ProductModel> GetById(int? id);
        Task SaveChanges();
    }
}

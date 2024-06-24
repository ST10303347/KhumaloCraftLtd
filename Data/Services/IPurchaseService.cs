using KhumaloCraftLtd.Models;
using System.Security.Cryptography;

namespace KhumaloCraftLtd.Data.Services
{
    public interface IPurchaseService
    {
        Task Add(Purchase purchase);
        IQueryable<Purchase> GetAll();
    }
}

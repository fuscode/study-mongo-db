using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public interface IMongoContext : IDisposable
    {
        Task<int> SaveChangesAsync();
        IMongoCollection<T> GetCollection<T>();
    }
}
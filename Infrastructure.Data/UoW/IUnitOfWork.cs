using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
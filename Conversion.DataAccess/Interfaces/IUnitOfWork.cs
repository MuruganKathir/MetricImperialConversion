using System;
using System.Threading.Tasks;

namespace Conversion.DataAccess.Interfaces
{
    public interface IUnitOfWork<out TContext> : IDisposable
    {
        TContext Context { get; }
        void Commit();

        Task CommitAsync();

        void Dispose(bool disposing);
    }
}
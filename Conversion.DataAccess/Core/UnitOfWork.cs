using System;
using System.Threading.Tasks;
using Conversion.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Conversion.DataAccess.Core
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext, new()
    {
        /// <summary>
        ///     Creates the UnitOfWork for the Given DbContext. If no DbContext is provided a
        ///     new instance is created based on the generic type paramaters.
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(TContext dbContext)
        {
            Context = dbContext ?? new TContext();
        }

        /// <summary>
        ///     The DbContext of type <typeparamref name="TContext" /> that is associated with this UnitOfWork.
        /// </summary>
        public TContext Context { get; private set; }

        #region Unit Of Work Methods

        /// <summary>
        ///     Commits the current changes to the database.
        /// </summary>
        public void Commit()
        {
            Context.SaveChanges();
        }

        /// <summary>
        ///     Commits the current changes to the database with Async.
        /// </summary>
        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     IDisposable Implementation. Disposes the database context if it hasnt already been disposed,
        ///     effectively reverting any uncommitted changes to the context.
        /// </summary>
        public void Dispose(bool disposing)
        {
            if (!disposing || Context == null)
                return;

            Context.Dispose();
            Context = null;
        }

        public void Dispose()
        {
            Dispose(true);

            /* 
             * Request the Garbage collector, not to finalize the context as it has already been cleaned up fully.
             * This is an optimization for the CLR.
             */
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
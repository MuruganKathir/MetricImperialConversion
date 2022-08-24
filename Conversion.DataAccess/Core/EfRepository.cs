using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Conversion.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Conversion.DataAccess.Core
{
    /// <summary>
    ///     A Genric Repository that implements the Unit Of Work pattern
    /// </summary>
    /// <typeparam name="TContext">The entitys database context of type DbContext</typeparam>
    /// <typeparam name="TEntity">The entity class representing a database Table</typeparam>
    public class EfRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext, new()
        where TEntity : class
    {
        #region Memebers

        protected IUnitOfWork<TContext> UnitOfWork;

        #endregion

        #region C'tor

        /// <summary>
        ///     Constructs the repository with the given unit of work.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public EfRepository(IUnitOfWork<TContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion

        #region Private Methods

        private DbSet<TEntity> Entities => UnitOfWork.Context.Set<TEntity>();

        private DbContext Context => UnitOfWork.Context;

        #endregion


        #region Repository Methods

        /// <summary>
        ///     Lazily Loads all the entities such that if further Linq statements are chained
        ///     the resulting query will only be executed at the database once.
        /// </summary>
        /// <remarks>
        ///     Whenever further Linq statements are applied to the IQueryable, the query is run at the database.
        ///     As opposed to an IEnumerable where the entire table is loaded and the filtering (query) happens in memory.
        /// </remarks>
        /// <returns>
        ///     All the rows in the database as an IQueryable of <typeparamref name="TEntity" />.
        /// </returns>
        public virtual IQueryable<TEntity> LazyGetAll()
        {
            return Entities.AsQueryable();
        }

        /// <summary>
        ///     Loads all the entities such that the entities are loaded into memory and any
        ///     subsequent filtering will occur in Memory.
        /// </summary>
        /// <returns>
        ///     All the rows in the database as an IEnumerable of <typeparamref name="TEntity" />.
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entities.AsEnumerable();
        }


        /// <summary>
        ///     Lazily Loads all the entities that match the given prdicate such that if further Linq statements are chained
        ///     the resulting query will only be executeed at the database once.
        /// </summary>
        /// <param name="predicate">
        ///     A lambda expression stipulating the filtering function
        /// </param>
        /// <returns>
        ///     The set of rows which match the given predicate Expression as an IQueryable of type <typeparamref name="TEntity" />
        /// </returns>
        public virtual IQueryable<TEntity> LazyGet(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        /// <summary>
        ///     Loads all the entities that match the given prdicate such that the entities are loaded into memory and any
        ///     subsequent filtering will occur in Memory.
        /// </summary>
        /// <param name="predicate">
        ///     An expression stipulating the filtering function
        /// </param>
        /// <returns>
        ///     The set of rows which match the given predicate Expression as an IEnumerable of type
        ///     <typeparamref name="TEntity" />
        /// </returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).AsEnumerable();
        }

        /// <summary>
        ///     Finds an entity with the given primary key.  If an entity with the
        ///     given primary key values exists in the context, then it is returned immediately
        ///     without making a request to the store. Otherwise, a request is made to the
        ///     store for an entity with the given primary key and this entity, if
        ///     found, is attached to the context and returned. If no entity is found in
        ///     the context or the store, then null is returned.
        /// </summary>
        /// <param name="keyValues">
        ///     The values of the primary key for the entity to be found.
        /// </param>
        /// <returns>
        ///     The entity of type <typeparamref name="TEntity" /> that was found, or null if no entity with the given keyValues
        ///     exists.
        /// </returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        /// <summary>
        ///    Adds the given entity to the context underlying the set in the Added state
        //     such that it will be inserted into the database when Commit is called on the associated UnitOfWork.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public virtual void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        ///     Adds a range of entities to the context underlying the set in the added state
        ///     such that it will be inserted into the database when Commit is called on the associated UnitOfWork.
        /// </summary>
        /// <param name="entities">An IEnumberable
        ///     <typeparam name="TEntity"></typeparam>
        ///     Entity
        /// </param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Insert(entity);
        }

        /// <summary>
        ///     Finds the Entity by id and marks it for deletion.
        /// </summary>
        /// <param name="id">The Id of the entity to delete.</param>
        public virtual void Delete(object id)
        {
            Delete(Find(id));
        }

        /// <summary>
        ///     Marks the given entity for deletion such that it will be added
        ///     to the database when Commit is called on the unitOfWork.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            AttachEntityToContext(entityToDelete);
            Entities.Remove(entityToDelete);
        }

        /// <summary>
        ///     Updates the given entity in the unerlying database context such that the
        ///     entity will be updated in the database when Commit is called on the UnitOfWork.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            AttachEntityToContext(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }


        /// <summary>
        ///     Attachs the entity to the current database context, if it has does not come from the current context.
        ///     An entity will be in the detatched state immediately after it has been created with the new operator.
        ///     If the entity is not in the detached state this will methods will act as a no-op.
        /// </summary>
        /// <remarks>
        ///     An entity can not be Attached to multiple contexts at once., nor can an entity cannot be attatched the the same
        ///     context twice.
        /// </remarks>
        /// <param name="entityToAttach">The entity to Attach to the current context.</param>
        /// <exception cref="InvalidOperationException">
        ///     Throws an InvalidOperationException when an entity with the same key
        ///     already exists in the context
        /// </exception>
        protected virtual void AttachEntityToContext(TEntity entityToAttach)
        {
            if (Context.Entry(entityToAttach).State == EntityState.Detached)
                Entities.Attach(entityToAttach);
        }

        #endregion
    }
}
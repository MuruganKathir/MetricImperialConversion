using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Conversion.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Lazily Loads all the entities such that if further Linq statements are chained
        ///     the resulting query will only be executeed at the database once.
        /// </summary>
        /// <remarks>
        ///     Whenever further Linq statements are applied to the IQueryable, the query is run at the database.
        ///     As opposed to an IEnumerable where the entire table is loaded and the filtering (query) happens in memory.
        /// </remarks>
        /// <returns>
        ///     All the rows in the database as an IQueryable of <typeparamref name="TEntity" />.
        /// </returns>
        IQueryable<TEntity> LazyGetAll();


        /// <summary>
        ///     Loads all the entities such that the entities are loaded into memory and any
        ///     subsequent filtering will occur in Memory.
        /// </summary>
        /// <returns>
        ///     All the rows in the database as an IEnumerable of <typeparamref name="TEntity" />.
        /// </returns>
        IEnumerable<TEntity> GetAll();


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
        IQueryable<TEntity> LazyGet(Expression<Func<TEntity, bool>> predicate);


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
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);


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
        ///     The entity found, or null.
        /// </returns>
        TEntity Find(params object[] keyValues);


        /// <summary>
        ///    Adds the given entity to the context underlying the set in the Added state
        //     such that it will be inserted into the database when Commit is called on the associated UnitOfWork.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void Insert(TEntity entity);


        /// <summary>
        ///     Adds a range of entities to the context underlying the set in the added state
        ///     such that it will be inserted into the database when Commit is called on the associated UnitOfWork.
        /// </summary>
        /// <param name="entities">An IEnumberable
        ///     <typeparam name="TEntity"></typeparam>
        ///     Entity
        /// </param>
        void InsertRange(IEnumerable<TEntity> entities);


        /// <summary>
        ///     Finds the Entity by id and marks it for deletion.
        /// </summary>
        /// <param name="id">The Id of the entity to delete.</param>
        void Delete(object id);


        /// <summary>
        ///     Marks the given entity for deletion such that it will be added
        ///     to the database when Commit is called on the unitOfWork.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void Delete(TEntity entityToDelete);


        /// <summary>
        ///     Updates the given entity in the unerlying database context such that the
        ///     entity will be updated in the database when Commit is called on the UnitOfWork.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        void Update(TEntity entityToUpdate);
    }
}
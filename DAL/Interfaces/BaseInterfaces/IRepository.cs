namespace DAL.Interfaces.BaseInterfaces;

/// <summary>
/// Default generic repository
/// </summary>
/// <typeparam name="T">Type of entity</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously gets all entities from db
    /// </summary>
    /// <returns>all entities</returns>
    Task<IQueryable<T>> GetAllAsync();

    /// <summary>
    /// Asynchronously gets entity by id from db
    /// </summary>
    /// <param name="id">Id of entity that should be found</param>
    /// <returns>Founded object</returns>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// Adds entity to db
    /// </summary>
    /// <param name="entity">Entity that should be added</param>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates entity in db
    /// </summary>
    /// <param name="entity">Entity that should be updated</param>
    void Update(T entity);

    /// <summary>
    /// Deletes entity in db
    /// </summary>
    /// <param name="id">Id of entity that should be deleted</param>
    void DeleteById(int id);
}

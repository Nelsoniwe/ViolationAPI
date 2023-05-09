namespace DAL.Interfaces.BaseInterfaces;

/// <summary>
/// Additional functionality to <see cref="IRepository{T}"/>
/// </summary>
/// <typeparam name="T">Type of entity</typeparam>
public interface IRepositoryExpanded<T> where T : class
{
    /// <summary>
    /// Asynchronously gets all entities with details from db
    /// </summary>
    Task<IQueryable<T>> GetAllWithDetailsAsync();

    /// <summary>
    /// Asynchronously gets entity with details by id from db
    /// </summary>
    /// <param name="id">Id of entity that should be found</param>
    Task<T> GetByIdWithDetailsAsync(int id);
}

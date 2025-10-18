namespace AbySalto.Mid.Domain.Interfaces;

/// <summary>
/// Generic repository interface for basic CRUD operations
/// </summary>
/// <typeparam name="T">Entity type (Product, Basket, etc.)</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Gets an entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity or null if not found</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Gets all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>Added entity</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity">Entity to update</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Deletes an entity by ID
    /// </summary>
    /// <param name="id">Entity ID to delete</param>
    Task DeleteByIdAsync(int id);
}

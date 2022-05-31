using Models;

namespace Services.Interfaces
{
    public interface IWebService<T> where T : Entity
    {
        Task<T?> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
        Task DeleteAsync(int id);
    }
}
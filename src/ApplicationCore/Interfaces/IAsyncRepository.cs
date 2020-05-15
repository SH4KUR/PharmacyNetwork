using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNetwork.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int? id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> ListAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task ExecuteSqlRawAsync(string query);
    }
}

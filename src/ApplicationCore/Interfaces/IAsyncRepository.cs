using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNetwork.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) TODO: clear

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        //Task<int> CountAsync(ISpecification<T> spec); TODO: clear
    }
}

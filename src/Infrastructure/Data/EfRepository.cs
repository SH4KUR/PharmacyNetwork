using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Interfaces;

namespace PharmacyNetwork.Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly PharmacyNetworkContext Context;

        public EfRepository(PharmacyNetworkContext context)
        {
            Context = context;
        }

        public async Task<T> GetByIdAsync(int id) //TODO: was added virtual in eShopWeb
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}

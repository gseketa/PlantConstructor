using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PlantConstructor.Domain.Services;
using PlantConstructor.Domain.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PlantConstructor.EntityFramework
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly PlantConstructorDbContextFactory _contextFactory;

        public GenericDataService(PlantConstructorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<T> Create(T entity)
        {
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task CreateMultiple(IEnumerable<T> entity)
        {
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                foreach (T ent in entity)
                {
                   await context.Set<T>().AddAsync(ent);                   
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }


        public async Task<T> Get(int id)
        {
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}

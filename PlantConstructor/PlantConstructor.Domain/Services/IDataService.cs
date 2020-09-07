using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlantConstructor.Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task CreateMultiple(IEnumerable<T> entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task DeleteBulk(List<T> listOfEntities);

    }
}

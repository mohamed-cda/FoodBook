using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);
        
        Task<bool> DeleteAsync(int id);
        
        Task<bool> UpdateAsync(TEntity entity);
        
        Task<TEntity> InsertAsync(TEntity entity);
    }
}

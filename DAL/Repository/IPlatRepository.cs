using BO.DTO;
using BO.DTO.Responses;
using BO.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IPlatRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Plat>> GetAllAsync();
        Task<PageResponse<Plat>> GetAllAsync(PageRequest pageRequest);
        Task<Plat> GetAsync(int id);
        Task<Plat> InsertAsync(Plat entity);
        Task<bool> UpdateAsync(Plat entity);
    }
}
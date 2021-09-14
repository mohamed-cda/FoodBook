using BO.DTO;
using BO.DTO.Responses;
using BO.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IIngredientRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<PageResponse<Ingredient>> GetAllAsync(PageRequest pageRequest);
        Task<Ingredient> GetAsync(int id);
        Task<Ingredient> InsertAsync(Ingredient entity);
        Task<bool> UpdateAsync(Ingredient entity);
    }
}
using BO.DTO;
using BO.DTO.Responses;
using BO.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IMenuRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<PageResponse<Menu>> GetAllAsync(PageRequest pageRequest);
        Task<Menu> GetAsync(int id);
        Task<Menu> GetByDateAsync(DateTime dateMenu, bool isMidi);
        Task<Menu> InsertAsync(Menu entity);
        Task<bool> UpdateAsync(Menu entity);
    }
}
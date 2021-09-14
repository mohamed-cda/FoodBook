using BO.DTO;
using BO.DTO.Responses;

using BO.Entity;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IRestaurantService
    {
        
        Task<Menu> CreateMenu(Menu menu);
        Task<Plat> CreatePlat(Plat reservation);
        Task<PageResponse<Ingredient>> GetAllIngredients(PageRequest pageRequest);
        Task<Ingredient> CreateIngredient(Ingredient ingredient);
        Task<Ingredient> ModifyIngredient(Ingredient ingredient);
        Task<bool> RemoveIngredientById(int id);
        Task<PageResponse<Menu>> GetAllMenus(PageRequest pageRequest);
        Task<PageResponse<Plat>> GetAllPlats(PageRequest pageRequest);
        Task<Menu> GetMenuById(int id);
        Task<Plat> GetAsync(int id);
        Task<Menu> ModifyMenu(Menu menu);
        Task<Plat> ModifyPlat(Plat plat);
        Task<bool> RemoveMenuById(int id);
        Task<bool> RemovePlatById(int id);
        Task<Menu> GetMenuByDate(DateTime DateMenu,bool isMidi);
    }
}
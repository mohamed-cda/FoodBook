using BO.DTO;
using BO.DTO.Responses;

using BO.Entity;
using DAL.Repository;
using DAL.UOW;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _db;

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        #region Menus
        public async Task<Menu> CreateMenu(Menu menu)
        {
            _db.BeginTransaction();
            IMenuRepository _menu = _db.GetRepository<IMenuRepository>();
            Menu newMenu = await _menu.InsertAsync(menu);
            _db.Commit();
            return newMenu;

        }

        public async Task<Menu> ModifyMenu(Menu menu)
        {
            _db.BeginTransaction();
            IMenuRepository _menu = _db.GetRepository<IMenuRepository>();
            var reussi = await _menu.UpdateAsync(menu);
            _db.Commit();

            if (reussi)
                return await Task.FromResult(menu);
            else
                return null;
        }

        public async Task<bool> RemoveMenuById(int id)
        {
            //La methode DeleteAsync est appeléee dans une transaction
            _db.BeginTransaction();
            IMenuRepository _menu = _db.GetRepository<IMenuRepository>();
            bool isDeleted = await _menu.DeleteAsync(id);
            _db.Commit();
            return isDeleted;
        }

        public async Task<PageResponse<Menu>> GetAllMenus(PageRequest pageRequest)
        {
            IMenuRepository _menu = _db.GetRepository<IMenuRepository>();

            return await _menu.GetAllAsync(pageRequest);

        }
        public async Task<Menu> GetMenuByDate(DateTime menuDate, bool isMidi)
        {
            IMenuRepository _Menus = _db.GetRepository<IMenuRepository>();

            return await _Menus.GetByDateAsync(menuDate,isMidi);
        }
        public async Task<PageResponse<Ingredient>> GetAllIngredients(PageRequest pageRequest)
        {
            IIngredientRepository _ingredient = _db.GetRepository<IIngredientRepository>();

            return await _ingredient.GetAllAsync(pageRequest);

        }
        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            _db.BeginTransaction();
            IIngredientRepository _ingredient = _db.GetRepository<IIngredientRepository>();
            Ingredient newIngredient = await _ingredient.InsertAsync(ingredient);
            _db.Commit();
            return newIngredient;

        }
        public async Task<Ingredient> ModifyIngredient(Ingredient ingredient)
        {
            _db.BeginTransaction();
            IIngredientRepository _ingredient = _db.GetRepository<IIngredientRepository>();
            var reussi = await _ingredient.UpdateAsync(ingredient);
            _db.Commit();

            if (reussi)
                return await Task.FromResult(ingredient);
            else
                return null;
        }
        public async Task<bool> RemoveIngredientById(int id)
        {
            _db.BeginTransaction();
            IIngredientRepository _ingredient = _db.GetRepository<IIngredientRepository>();
           
            bool isDeleted = await _ingredient.DeleteAsync(id);
            _db.Commit();
            return isDeleted;
        }
        public async Task<Menu> GetMenuById(int id)
        {

            IMenuRepository _Menus = _db.GetRepository<IMenuRepository>();

            return await _Menus.GetAsync(id);
        }
        public async Task<Plat> CreatePlat(Plat plat)
        {
            _db.BeginTransaction();
            IPlatRepository _plat = _db.GetRepository<IPlatRepository>();
            Plat newPlat = await _plat.InsertAsync(plat);
            _db.Commit();
            return newPlat;

        }

        public async Task<Plat> ModifyPlat(Plat plat)
        {
            _db.BeginTransaction();
            IPlatRepository _plat = _db.GetRepository<IPlatRepository>();
            var reussi = await _plat.UpdateAsync(plat);
            _db.Commit();

            if (reussi)
                return await Task.FromResult(plat);
            else
                return null;
        }

        public async Task<bool> RemovePlatById(int id)
        {
            _db.BeginTransaction();
            IPlatRepository _plat = _db.GetRepository<IPlatRepository>();
            bool isDeleted = await _plat.DeleteAsync(id);
            _db.Commit();
            return isDeleted;
        }

        public async Task<PageResponse<Plat>> GetAllPlats(PageRequest pageRequest)
        {
            IPlatRepository _plat = _db.GetRepository<IPlatRepository>();

            return await _plat.GetAllAsync(pageRequest);

        }

        public async Task<Plat> GetAsync(int id)
        {

            IPlatRepository _Plats = _db.GetRepository<IPlatRepository>();

            return await _Plats.GetAsync(id);
        }







        #endregion
    }


}

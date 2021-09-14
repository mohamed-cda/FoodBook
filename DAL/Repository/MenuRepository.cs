using DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BO.DTO.Responses;
using BO.Entity;
using BO.DTO;

namespace DAL.Repository
{
    class MenuRepository : IMenuRepository
    {
        private readonly DbSession _session;
        private readonly ILogger<MenuRepository> _logger;
        public MenuRepository(DbSession dbSession, ILogger<MenuRepository> logger)
        {
            _session = dbSession;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var stmt = @"delete  from Menu 
                       where id=@Id_menu";
            int i = await _session.Connection.ExecuteAsync(stmt, new {Id=@id }, _session.Transaction);
            return i > 0;
        }

        public async Task<PageResponse<Menu>> GetAllAsync(PageRequest pageRequest)
        {
            var stmt = @"select * from Menu 
                        ORDER BY Id_menu
                        OFFSET @PageSize * (@Page - 1) rows
                        FETCH NEXT @PageSize rows only";
            string queryCount = " SELECT COUNT(*) FROM Menu";
            IEnumerable<Menu> menuTask = await _session.Connection.QueryAsync<Menu>(stmt, pageRequest, _session.Transaction);
            int countTask = await _session.Connection.ExecuteScalarAsync<int>(queryCount, null, _session.Transaction);
            return new PageResponse<Menu>(pageRequest.Page, pageRequest.PageSize, countTask, (menuTask).ToList());
        }

        public async Task<Menu> GetAsync(int id)
        {
            //Evité l'injection sql avec des reqêtes paramétrées
            var stmt = @"select * from Menu where Id = @id";
            return await _session.Connection.QueryFirstOrDefaultAsync<Menu>(stmt, new { Id = id }, _session.Transaction);
        }
        public async Task<Menu> GetByDateAsync(DateTime dateMenu, bool isMidi)
        {
            //Evité l'injection sql avec des reqêtes paramétrées
            var stmt = @"SELECT m.*, p.*, tp.* FROM Menu AS m
                        LEFT JOIN Composer c ON c.Id_menu= m.Id_menu
                        LEFT JOIN Plat p ON p.Id_plat=c.Id_plat
                        LEFT JOIN TypePlat tp ON p.Id_type=tp.Id_type
                        WHERE m.date = @dateDuMenu AND m.IsMidi= @IsMidi
                         ORDER BY p.Id_type";

            var menus = await _session.Connection.QueryAsync<Menu, Plat, TypePlat, Menu>(stmt,(menu, plat, typePlat) => {

                menu.Plats = menu.Plats ?? new List<Plat>();
                plat.Type = typePlat;
                menu.Plats.Add(plat);
                return menu;

            },  new { dateDuMenu = dateMenu,IsMidi=isMidi }, _session.Transaction, splitOn: "Id_Plat,Id_Type");

            return menus.GroupBy(m => m.Id_menu).Select(mg =>
            {
                var menuFinal = mg.FirstOrDefault();
                menuFinal.Plats = mg.Where(mm => mm.Plats.Count > 0).Select(mm => mm.Plats.FirstOrDefault()).ToList();
                return menuFinal;

            }).FirstOrDefault();

        }

        public async Task<Menu> InsertAsync(Menu entity)
        {
            var stmt = @"insert into Menu(IsMidi,Date) output INSERTED.Id
            values (@IsMidi, @dateDuMenu);";
            int i = await _session.Connection.QuerySingleAsync<int>(stmt, new { dateDuMenu = entity.Date}, _session.Transaction);
            return await GetAsync(i);
        }

        public async Task<bool> UpdateAsync(Menu entity)
        {
            
            var stmt = @"update Menu
                set IsMidi=@isMidi, Date=@dateDuMenu
                where Id_menu=@Id";

            //nb Affected row
            int i = await _session.Connection.ExecuteAsync(stmt, new { Id=entity.Id_menu,dateDuMenu = entity.Date,isMidi=entity.IsMidi}, _session.Transaction);
            return i > 0;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            var stmt = @"select * from menu";
            return await _session.Connection.QueryAsync<Menu>(stmt, null, _session.Transaction);
        }


    }
}

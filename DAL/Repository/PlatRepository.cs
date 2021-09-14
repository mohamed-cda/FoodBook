using DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BO.DTO;
using BO.Entity;
using BO.DTO.Responses;

namespace DAL.Repository
{
    class PlatRepository : IPlatRepository
    {
        private readonly DbSession _session;
        private readonly ILogger<PlatRepository> _logger;
        public PlatRepository(DbSession dbSession, ILogger<PlatRepository> logger)
        {
            _session = dbSession;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var stmt = @"delete  from Plat 
                       where Id_plat=@Id";
            int i = await _session.Connection.ExecuteAsync(stmt, new { Id = @id }, _session.Transaction);
            return i > 0;
        }
        public async Task<IEnumerable<Plat>> GetAllAsync()
        {
            var stmt = @"select * from Plat";
            return await _session.Connection.QueryAsync<Plat>(stmt, null, _session.Transaction);
        }
        public async Task<PageResponse<Plat>> GetAllAsync(PageRequest pageRequest)
        {
            var stmt = @"SELECT * FROM Plat AS p 
                        JOIN TypePlat AS tp ON p.Id_type = tp.Id_type
                        ORDER BY p.nom
                        OFFSET @PageSize * (@Page - 1) rows
                        FETCH NEXT @PageSize rows only";
            string queryCount = " SELECT COUNT(*) FROM Plat ";

            IEnumerable<Plat> platTask = await _session.Connection.QueryAsync<Plat, TypePlat, Plat>(stmt, (plat, type) =>
            {
                plat.Type = type;
                return plat;
            },pageRequest, _session.Transaction, splitOn: "Id_Type");

            int countTask = await _session.Connection.ExecuteScalarAsync<int>(queryCount, null, _session.Transaction);
            return new PageResponse<Plat>(pageRequest.Page, pageRequest.PageSize, countTask, (platTask).ToList());
        }

        public async Task<Plat> GetAsync(int id)
        {
            //Eviter l'injection sql avec des reqêtes paramétrées

            var stmt = @"SELECT p.*, i.* FROM Plat AS p
                        LEFT JOIN Contenir c ON c.Id_plat = p.Id_plat
                        LEFT JOIN Ingredient i ON i.Id_ingredient = c.Id_ingredient
                        
                        WHERE p.Id_plat = @id_du_plat";

            var plats = await _session.Connection.QueryAsync<Plat, Ingredient, Plat>(stmt, (plat,ingredient) => {

                plat.Ingredient = plat.Ingredient ?? new List<Ingredient>();
                
                plat.Ingredient.Add(ingredient);
                return plat;

            }, new { id_du_plat = id}, _session.Transaction, splitOn: "Id_ingredient");

            return plats.GroupBy(p => p.Id_plat).Select(pg =>
            {
                var platFinal = pg.FirstOrDefault();
                platFinal.Ingredient = pg.Where(mm => mm.Ingredient.Count > 0).Select(mm => mm.Ingredient.FirstOrDefault()).ToList();
                return platFinal;

            }).FirstOrDefault();


           
        }
        

        public async Task<Plat> InsertAsync(Plat entity)
        {
            var stmt = @"insert into Plat(Nom,Id_type) output INSERTED.Id_plat
            values (@nom,@Id)";
            int i = await _session.Connection.QuerySingleAsync<int>(stmt, new { nom = entity.Nom, Id = entity.Id_Type }, _session.Transaction); 
            return await GetAsync(i);
        }



        public async Task<bool> UpdateAsync(Plat entity)
        {
            var stmt = @"update Plat
                set Nom=@Nom,TypePlat=@TypePlat
                where Id=@Id";

            //nb Affected row
            int i = await _session.Connection.ExecuteAsync(stmt, entity, _session.Transaction);
            return i > 0;
        }



    }
}

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
    class IngredientRepository : IIngredientRepository
    {
        private readonly DbSession _session;
        private readonly ILogger<IngredientRepository> _logger;
        public IngredientRepository(DbSession dbSession, ILogger<IngredientRepository> logger)
        {
            _session = dbSession;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var stmt = @"delete  from Ingredient 
                       where id=@Id_ingredient";
            int i = await _session.Connection.ExecuteAsync(stmt, new { Id_ingredient = @id }, _session.Transaction);
            return i > 0;
        }

        public async Task<PageResponse<Ingredient>> GetAllAsync(PageRequest pageRequest)
        {
            var stmt = @"select * from Ingredient 
                        ORDER BY Id_ingredient
                        OFFSET @PageSize * (@Page - 1) rows
                        FETCH NEXT @PageSize rows only";
            string queryCount = " SELECT COUNT(*) FROM Ingredient";
            IEnumerable<Ingredient> IngredientTask = await _session.Connection.QueryAsync<Ingredient>(stmt, pageRequest, _session.Transaction);
            int countTask = await _session.Connection.ExecuteScalarAsync<int>(queryCount, null, _session.Transaction);
            return new PageResponse<Ingredient>(pageRequest.Page, pageRequest.PageSize, countTask, (IngredientTask).ToList());
        }

        public async Task<Ingredient> GetAsync(int id)
        {
            //Evité l'injection sql avec des reqêtes paramétrées
            var stmt = @"select * from Ingredient where Id_ingredient = @id";
            return await _session.Connection.QueryFirstOrDefaultAsync<Ingredient>(stmt, new { Id_ingredient = id }, _session.Transaction);
        }

        public async Task<Ingredient> InsertAsync(Ingredient entity)
        {
            var stmt = @"insert into Ingredient(Nom,Prix) output INSERTED.Id_ingredient
            values (@Nom, @Prix);";
            int i = await _session.Connection.QuerySingleAsync<int>(stmt, entity, _session.Transaction);
            return await GetAsync(i);
        }

        public async Task<bool> UpdateAsync(Ingredient entity)
        {
            int id = entity.Id_ingredient;
            var stmt = @"update ingredient
                set Nom=@Nom, Prix=@Prix
                where id=@Id_ingredient";

            //nb Affected row
            int i = await _session.Connection.ExecuteAsync(stmt, entity, _session.Transaction);
            return i > 0;
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            var stmt = @"select * from ingredient";
            return await _session.Connection.QueryAsync<Ingredient>(stmt, null, _session.Transaction);
        }


    }
}

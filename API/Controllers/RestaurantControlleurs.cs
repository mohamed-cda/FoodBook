using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using BO.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using BO.Entity;
using BO.DTO;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class RestaurantControlleur : ControllerBase
    {

        //Service qui gère le réstaurant service
        private readonly IRestaurantService _restaurantService;

        public RestaurantControlleur(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }


        /// <summary>
        /// Permet de récupérer la liste des plats
        /// </summary>
        /// <returns>La liste des plats</returns>
        [HttpGet("plat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<PageResponse<Plat>>> GetAll([FromQuery] PageRequest pageRequest)
        {

            //return Ok(await _resetaurantService.GetAllPlats());
            return Ok(await _restaurantService.GetAllPlats(pageRequest));
        }

        /// <summary>
        /// Permert de récupérer un livre avec son identifiant unique
        /// </summary>
        /// <param name="id">Identifiant unique du livre</param>
        /// <returns>Renvoi le livre définit par l'identifiant unique</returns>
        [HttpGet("plat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlatById([FromRoute] int id)
        {
            Plat plat = await _restaurantService.GetAsync(id);
            if (plat == null)
            {
                return NotFound(); // StatusCode = 404
            }
            else
            {
                return Ok(plat); // StatusCode = 200
            }
        }
        


        /// <summary>
        /// Créer une Plat et l'ajouter en BDD
        /// </summary>
        /// <param name="plat">Livre à ajouter sans l'identifiant unique</param>
        /// <returns>Renvoi le livre avec l'identifiant généré</returns>
        [HttpPost("Plat")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePlat([FromBody] Plat Plat)
        {
            // Ajout d'une Plat avec la bll server
            Plat newPlat = await _restaurantService.CreatePlat(Plat);
            if (newPlat != null)
            {
                // Créer une redirection vers GetPlatById(newBook.BookId);
                // return CreatedAtAction(nameof(GetPlatById), new { id = newPlat.Id_Plat }, newPlat);
                return CreatedAtAction(nameof(GetPlatById), new { id = newPlat.Id_plat }, newPlat);
            }
            else
            {
                // Retourne un code 400  Bad Request
                return BadRequest();
            }
        }


        [HttpDelete("plat/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlat([FromRoute] int id)
        {
            if (await _restaurantService.RemovePlatById(id))
            {
                // Renvoie un code 204 aucun contenu
                return NoContent();
            }
            else
            {
                // Renvoie un code 404: la ressource est introuvable
                return NotFound();
            }
        }


        [HttpPut("plat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ModifyPlat([FromRoute] int id, [FromBody] Plat Plat)
        {
           if (Plat == null || id != Plat.Id_plat)
            
            {
                // Retourne un code 400  Bad Request
                return BadRequest();
            }
            else
            {
                Plat PlatModified = await _restaurantService.ModifyPlat(Plat);
                if (PlatModified != null)
                {
                    // Renvoie la ressource modifiée
                    return Ok(PlatModified);
                }
                else
                {
                    // Renvoie un code 404: la ressource est introuvable
                    return NotFound();
                }
            }
        }
        [HttpGet("Menu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<PageResponse<Menu>>> GetAllMenu([FromQuery] PageRequest pageRequest)
        {
          return Ok(await _restaurantService.GetAllMenus(pageRequest));
        }
        [HttpPost("menu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<Menu>> PostMenu([FromBody] Menu menu)
        {

            //return Ok(await _resetaurantService.GetAllPlats());
            return Ok(await _restaurantService.CreateMenu(menu));
        }
        [HttpPut("menu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<Menu>> ModifyMenu([FromBody] Menu menu)
        {

           
            return Ok(await _restaurantService.ModifyMenu(menu));
        }

        [HttpGet("menu/date")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<Menu>> GetMenuByDate([FromQuery] DateTime dateMenu,bool isMidi)
        {
            return Ok(await _restaurantService.GetMenuByDate(dateMenu,isMidi));
        }

        [HttpDelete("menu/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
            if (await _restaurantService.RemoveMenuById(id))
            {
                // Renvoie un code 204 aucun contenu
                return NoContent();
            }
            else
            {
                // Renvoie un code 404: la ressource est introuvable
                return NotFound();
            }
        }

        [HttpGet("ingredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<PageResponse<Ingredient>>> GetAllIngredients([FromQuery] PageRequest pageRequest)
        {

            //return Ok(await _resetaurantService.GetAllPlats());
            return Ok(await _restaurantService.GetAllIngredients(pageRequest));
        }
        [HttpPost("ingredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<Ingredient>> CreatIngredient([FromBody] Ingredient ingredient)
        {

            //return Ok(await _resetaurantService.GetAllPlats());
            return Ok(await _restaurantService.CreateIngredient(ingredient));
        }
        [HttpPut("ingredient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ModifyIngredient([FromRoute] int id, [FromBody] Ingredient ingredient)
        {
            if (ingredient == null || id !=ingredient.Id_ingredient)

            {
                // Retourne un code 400  Bad Request
                return BadRequest();
            }
            else
            {
                Ingredient ingredientModified = await _restaurantService.ModifyIngredient(ingredient);
                if (ingredientModified != null)
                {
                    // Renvoie la ressource modifiée
                    return Ok(ingredientModified);
                }
                else
                {
                    // Renvoie un code 404: la ressource est introuvable
                    return NotFound();
                }
            }
        }
        [HttpDelete("ingredient/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIngredient([FromRoute] int id)
        {
            if (await _restaurantService.RemoveIngredientById(id))
            {
                // Renvoie un code 204 aucun contenu
                return NoContent();
            }
            else
            {
                // Renvoie un code 404: la ressource est introuvable
                return NotFound();
            }
        }
    }
}


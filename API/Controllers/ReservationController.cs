using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Http;


using BO.DTO.Responses;

using BO.DTO;
using BO.Entity;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ReservationController : ControllerBase
    {

        //Service qui gère la librairi
        private readonly IReservationService _reservationService = null;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


    
        /// <summary>
        /// Permert de récupérer un livre avec son identifiant unique
        /// </summary>
        /// <param name="id">Identifiant unique du livre</param>
        /// <returns>Renvoi le livre définit par l'identifiant unique</returns>
        [HttpGet("Reservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllResrvations([FromQuery] PageRequest pageRequest)
        {
            return Ok(await _reservationService.GetAllReservations(pageRequest));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationById([FromRoute] int id)
        {
            Reservation reservation = await _reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound(); // StatusCode = 404
            }
            else
            {
                return Ok(reservation); // StatusCode = 200
            }
        }
        [HttpGet("ListeMenu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // [Authorize(Roles="Admin")]
        public async Task<ActionResult<PageResponse<Menu>>> GetAllMenu([FromQuery] PageRequest pageRequest)
        {

            //return Ok(await _resetaurantService.GetAllPlats());
            return Ok(await _reservationService.GetAllMenus(pageRequest));
        }

        /// <summary>
        /// Créer une reservation et l'ajouter en BDD
        /// </summary>
        /// <param name="reservation">Livre à ajouter sans l'identifiant unique</param>
        /// <returns>Renvoi le livre avec l'identifiant généré</returns>
        [HttpPost("Reservation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
        {
            // Ajout d'une reservation avec la bll server
            Reservation newReservation = await _reservationService.CreateReservation(reservation);
            if (newReservation != null)
            {
                // Créer une redirection vers GetReservationById(newBook.BookId);
                // return CreatedAtAction(nameof(GetReservationById), new { id = newReservation.Id_reservation }, newReservation);
                return CreatedAtAction(nameof(GetReservationById), new { id = 2 }, newReservation);
            }
            else
            {
                // Retourne un code 400  Bad Request
                return BadRequest();
            }
        }


        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        //{
        //    if (await _reservationService.RemoveReservationById(id))
        //    {
        //        // Renvoie un code 204 aucun contenu
        //        return NoContent();
        //    }
        //    else
        //    {
        //        // Renvoie un code 404: la ressource est introuvable
        //        return NotFound();
        //    }
       // }


        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> ModifyReservation([FromRoute] int id, [FromBody] Reservation reservation)
        //{
        //   // if (reservation == null || id != reservation.Id_reservation)
        //        if (reservation == null || id !=2)
        //        {
        //        // Retourne un code 400  Bad Request
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        Reservation reservationModified = await _reservationService.ModifyReservation(reservation);
        //        if (reservationModified != null)
        //        {
        //            // Renvoie la ressource modifiée
        //            return Ok(reservationModified);
        //        }
        //        else
        //        {
        //            // Renvoie un code 404: la ressource est introuvable
        //            return NotFound();
        //        }
        //    }
        //}

    }
}


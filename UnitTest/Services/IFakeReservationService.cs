using BO.DTO;
using BO.DTO.Responses;
using BO.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    interface IFakeReservationService
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task<List<Reservation>> GetAllReservations();
        Task<PageResponse<Reservation>> GetAllReservations(PageRequest pageRequest);
        Task<Reservation> GetReservationById(int id);
        Task<Reservation> ModifyReservation(Reservation reservation);
        Task<bool> RemoveReservationById(int id);
    }
}
using BO.DTO;
using BO.DTO.Responses;
using BO.Entity;
using System.Threading.Tasks;

namespace BLL.Services
{
   public  interface IReservationService
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task<PageResponse<Menu>> GetAllMenus(PageRequest pageRequest);
        Task<PageResponse<Reservation>> GetAllReservations(PageRequest pageRequest);
        Task<Reservation> GetReservationById(int id);
        Task<Reservation> ModifyReservation(Reservation reservation);
        Task<bool> RemoveReservationById(int id);
    }
}
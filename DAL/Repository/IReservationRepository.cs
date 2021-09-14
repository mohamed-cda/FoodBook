
using BO.Entity;

namespace DAL.Repository
{
    public interface IReservationRepository : IGenericRepository<Reservation>, IPageableRepository<Reservation>
    {
    }
}

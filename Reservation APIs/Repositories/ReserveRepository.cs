using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ReserveRepository : GenericRepository<Reserve>, IReserveRepository
    {
        public ReserveRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

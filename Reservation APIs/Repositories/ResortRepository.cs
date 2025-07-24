using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ResortRepository : GenericRepository<Resort>, IResortRepository
    {
        public ResortRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

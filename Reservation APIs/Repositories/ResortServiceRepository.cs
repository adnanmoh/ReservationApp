using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ResortServiceRepository : GenericRepository<ResortService>, IResortServiceRepository
    {
        public ResortServiceRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

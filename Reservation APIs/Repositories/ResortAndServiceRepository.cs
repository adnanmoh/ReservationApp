using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ResortAndServiceRepository : GenericRepository<ResortAndService>, IResortAndServiceRepository
    {
        public ResortAndServiceRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

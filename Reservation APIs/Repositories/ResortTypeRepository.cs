using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ResortTypeRepository : GenericRepository<ResortType>, IResortTypeRepository
    {
        public ResortTypeRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

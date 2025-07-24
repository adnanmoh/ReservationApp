using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ResortsPhotoRepository : GenericRepository<ResortsPhoto>, IResortsPhotoRepository
    {
        public ResortsPhotoRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class TypesOfServiceRepository : GenericRepository<TypesOfService>, ITypesOfServiceRepository
    {
        public TypesOfServiceRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

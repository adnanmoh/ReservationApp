using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class FinancialAccountRepository : GenericRepository<FinancialAccount>, IFinancialAccountRepository
    {
        public FinancialAccountRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

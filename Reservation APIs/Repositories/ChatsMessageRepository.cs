using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class ChatsMessageRepository : GenericRepository<ChatsMessage>, IChatsMessageRepository
    {
        public ChatsMessageRepository(ReservationAppContext context) : base(context)
        {
        }
    }
}

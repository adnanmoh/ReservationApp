using Microsoft.EntityFrameworkCore;
using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;
using System;
using System.Linq.Expressions;

namespace Reservation_APIs.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {

        public ChatRepository(ReservationAppContext context) : base(context)
        {
        }

       
    }
}

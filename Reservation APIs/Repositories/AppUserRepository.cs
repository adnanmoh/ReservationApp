using Microsoft.EntityFrameworkCore;
using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;

namespace Reservation_APIs.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        private readonly ReservationAppContext context;

        public AppUserRepository(ReservationAppContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<bool> LogIn(int id, string password)
        {
            var user = await context.AppUsers.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            return VerifyPasswordHash(user.Password, password);
        }

        private bool VerifyPasswordHash(string passwordHash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}

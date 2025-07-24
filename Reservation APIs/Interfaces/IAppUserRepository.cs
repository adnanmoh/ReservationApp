using Reservation_APIs.Models;

namespace Reservation_APIs.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<bool> LogIn(int id, string password);
    }
}

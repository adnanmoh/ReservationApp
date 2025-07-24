namespace Reservation_APIs.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(string message);
        /*Task SandMessage(string message);*/

    }
}

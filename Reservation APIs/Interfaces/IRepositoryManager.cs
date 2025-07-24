namespace Reservation_APIs.Interfaces
{
    public interface IRepositoryManager
    {
        IAppUserRepository AppUserRepository { get; }
        IChatRepository ChatRepository { get; }
        IChatsMessageRepository ChatsMessageRepository { get; }
        IFinancialAccountRepository FinancialAccountRepository { get; }
        IReserveRepository ReserveRepository { get; }
        IResortRepository ResortRepository { get; }
        IResortServiceRepository ResortServiceRepository { get; }
        IResortsPhotoRepository ResortsPhotoRepository { get; }
        IResortTypeRepository ResortTypeRepository { get; }
        ITypesOfServiceRepository TypesOfServiceRepository { get; }
        IUserTypeRepository UserTypeRepository { get; }
        IResortAndServiceRepository ResortAndServiceRepository { get; }
    }
}

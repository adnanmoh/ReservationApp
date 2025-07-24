using Reservation_APIs.Interfaces;

namespace Reservation_APIs.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly IChatRepository chatRepository;
        private readonly IChatsMessageRepository chatsMessageRepository;
        private readonly IFinancialAccountRepository financialAccountRepository;
        private readonly IReserveRepository reserveRepository;
        private readonly IResortRepository resortRepository;
        private readonly IResortServiceRepository resortServiceRepository;
        private readonly IResortsPhotoRepository resortsPhotoRepository;
        private readonly IResortTypeRepository resortTypeRepository ;
        private readonly ITypesOfServiceRepository typesOfServiceRepository ;
        private readonly IUserTypeRepository userTypeRepository;
        private readonly IResortAndServiceRepository resortAndServiceRepository;
        public RepositoryManager(
                IAppUserRepository appUserRepository,
                IChatRepository chatRepository,
                IChatsMessageRepository chatsMessageRepository,
                IFinancialAccountRepository financialAccountRepository,
                IReserveRepository reserveRepository,
                IResortRepository resortRepository,
                IResortServiceRepository resortServiceRepository,
                IResortsPhotoRepository resortsPhotoRepository,
                IResortTypeRepository resortTypeRepository ,
                ITypesOfServiceRepository typesOfServiceRepository ,
                IUserTypeRepository userTypeRepository,
                IResortAndServiceRepository resortAndServiceRepository
            )
        {
            this.appUserRepository = appUserRepository;
            this.chatRepository = chatRepository;
            this.chatsMessageRepository = chatsMessageRepository;
            this.financialAccountRepository = financialAccountRepository;
            this.reserveRepository = reserveRepository;
            this.resortRepository = resortRepository;
            this.resortServiceRepository = resortServiceRepository;
            this.resortsPhotoRepository = resortsPhotoRepository;
            this.resortTypeRepository = resortTypeRepository;
            this.typesOfServiceRepository = typesOfServiceRepository;
            this.userTypeRepository = userTypeRepository;
            this.resortAndServiceRepository = resortAndServiceRepository;
        }
        public IAppUserRepository AppUserRepository => appUserRepository;

        public IChatRepository ChatRepository => chatRepository;

        public IChatsMessageRepository ChatsMessageRepository => chatsMessageRepository;

        public IFinancialAccountRepository FinancialAccountRepository => financialAccountRepository;

        public IReserveRepository ReserveRepository =>  reserveRepository;

        public IResortRepository ResortRepository =>resortRepository;

        public IResortServiceRepository ResortServiceRepository => resortServiceRepository;

        public IResortsPhotoRepository ResortsPhotoRepository => resortsPhotoRepository;

        public IResortTypeRepository ResortTypeRepository => resortTypeRepository;

        public ITypesOfServiceRepository TypesOfServiceRepository => typesOfServiceRepository;

        public IUserTypeRepository UserTypeRepository => userTypeRepository;

        public IResortAndServiceRepository ResortAndServiceRepository => resortAndServiceRepository;
    }
}

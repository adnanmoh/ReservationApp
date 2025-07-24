using AutoMapper;
using Reservation_APIs.Controllers;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.MapperHelper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<AppUserDTO, AppUser>();

            CreateMap<Chat, ChatDTO>();
            CreateMap<ChatDTO, Chat>();

            CreateMap<ChatsMessage, ChatsMessageDTO>();
            CreateMap<ChatsMessageDTO, ChatsMessage>();

            CreateMap<FinancialAccount, FinancialAccountDTO>();
            CreateMap<FinancialAccountDTO, FinancialAccount>();


            CreateMap<Reserve, ReserveDTO>();
            CreateMap<ReserveDTO, Reserve>();

            CreateMap<Resort, ResortDTO>();
            CreateMap<ResortDTO, Resort>();

            CreateMap<ResortService, ResortServiceDTO>();
            CreateMap<ResortServiceDTO, ResortService>();


            CreateMap<ResortsPhoto, ResortsPhotoDTO>();
            CreateMap<ResortsPhotoDTO, ResortsPhoto>();

            CreateMap<ResortType, ResortTypeDTO>();
            CreateMap<ResortTypeDTO, ResortType>();

            CreateMap<TypesOfService, TypesOfServiceDTO>();
            CreateMap<TypesOfServiceDTO, TypesOfService>();

            CreateMap<UserType, UserTypeDTO>();
            CreateMap<UserTypeDTO, UserType>();

            CreateMap<ResortAndService, ResortAndServiceDTO>();
            CreateMap<ResortAndServiceDTO, ResortAndService>();


        }
    }
}

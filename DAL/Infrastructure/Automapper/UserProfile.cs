using AutoMapper;
using DAL.Infrastructure.Models;
using Domain.Models;

namespace DAL.Infrastructure.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserProfileInfo, Domain.Models.ReaderCard>()
                .ReverseMap();

            CreateMap<RegisterUserModel, User>()
                .ReverseMap();

            CreateMap<RegisterUserModel, Domain.Models.ReaderCard>()
                .ReverseMap();
        }
    }
}
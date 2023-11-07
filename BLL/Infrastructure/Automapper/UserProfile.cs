using AutoMapper;
using BLL.Infrastructure.Models;
using Domain.Models;

namespace BLL.Infrastructure.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserProfileModel>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.ReaderCard.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.ReaderCard.LastName))
                .ForMember(dst => dst.ProfilePicture, opt => opt.MapFrom(src => src.ReaderCard.ProfilePicture))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.ReaderCard.Address))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.ReaderCard.PhoneNumber))
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.ReaderCard.BirthDate))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.ReaderCard.Email))
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ReverseMap();
        }
    }
}
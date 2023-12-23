using AutoMapper;
using Domain.Models;

namespace DAL.Infrastructure.Automapper;
public class EnumItemProfile : Profile
{
    public EnumItemProfile()
    {
        CreateMap<EnumItem, EnumItem>()
            .ForMember(dst => dst.Enum,
                opt => opt.Ignore());
    }
}

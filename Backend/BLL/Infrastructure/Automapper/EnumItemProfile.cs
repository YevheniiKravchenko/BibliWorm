using AutoMapper;
using BLL.Infrastructure.Models.EnumItem;
using Domain.Models;

namespace BLL.Infrastructure.Automapper;
public class EnumItemProfile : Profile
{
    public EnumItemProfile()
    {
        CreateMap<EnumItem, EnumItemModel>()
            .ReverseMap();
    }
}

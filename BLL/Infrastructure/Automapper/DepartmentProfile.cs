using AutoMapper;
using BLL.Infrastructure.Models.Department;
using Domain.Models;

namespace BLL.Infrastructure.Automapper;
public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentModel>()
            .ForMember
            (
                dst => dst.PeopleAttendedForLastMonth,
                opt => opt.MapFrom
                (
                    src => src.DepartmentStatistics
                        .Where(x => x.RecordDate >= DateTime.Now.AddMonths(-1))
                        .Sum(ds => ds.NumberOfPeopleAttended)
                )
            );

        CreateMap<CreateUpdateDepartmentModel, DepartmentModel>();

        CreateMap<CreateDepartmentStatisticsModel, DepartmentStatistics>();
    }
}

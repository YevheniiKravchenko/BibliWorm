using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models.Department;
using DAL.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Lazy<IMapper> _mapper;

    public DepartmentService(
        IUnitOfWork unitOfWork,
        Lazy<IMapper> mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void AddDepartment(CreateUpdateDepartmentModel departmentModel)
    {
        var department = _mapper.Value.Map<Department>(departmentModel);

        _unitOfWork.Departments.Value.Create(department);
    }

    public void AddStatisticsForDepartment(CreateDepartmentStatisticsModel departmentStatisticsModel)
    {
        var departmentStatistics = _mapper.Value.Map<DepartmentStatistics>(departmentStatisticsModel);

        _unitOfWork.Departments.Value.CreateDepartmentStatistics(departmentStatistics);
    }

    public void DeleteDepartment(int departmentId)
    {
        _unitOfWork.Departments.Value.Delete(departmentId);
    }

    public IEnumerable<DepartmentModel> GetAllDepartments()
    {
        var departments = _unitOfWork.Departments.Value.GetAll()
            .Include(d => d.DepartmentStatistics);
        var departmentModels = _mapper.Value.Map<List<DepartmentModel>>(departments);

        return departmentModels;
    }

    public DepartmentModel GetDepartmentById(int departmentId)
    {
        var department = _unitOfWork.Departments.Value.GetById(departmentId);
        var departmentModel = _mapper.Value.Map<DepartmentModel>(department);

        return departmentModel;
    }

    public void UpdateDepartment(CreateUpdateDepartmentModel departmentModel)
    {
        var department = _mapper.Value.Map<Department>(departmentModel);

        _unitOfWork.Departments.Value.Update(department);
    }
}

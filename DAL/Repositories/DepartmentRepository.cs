using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class DepartmentRepository : IDepartmentRepository
{
    private readonly DbContextBase _dbContext;
    private readonly Lazy<IMapper> _mapper;

    private readonly DbSet<Department> _departments;
    private readonly DbSet<DepartmentStatistics> _departmentStatistics;

    public DepartmentRepository(
        DbContextBase dbContext,
        Lazy<IMapper> mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _departments = dbContext.Departments;
        _departmentStatistics = dbContext.DepartmentStatistics;
    }

    public void Create(Department newDepartment)
    {
        _departments.Add(newDepartment);
        _dbContext.Commit();
    }

    public void CreateDepartmentStatistics(DepartmentStatistics departmentStatistics)
    {
        var department = _departments.FirstOrDefault(d => d.DepartmentId == departmentStatistics.DepartmentId)
            ?? throw new ArgumentException("INVALID_DEPARTMENT_ID");

        _departmentStatistics.Add(departmentStatistics);
        _dbContext.Commit();
    }

    public void Delete(int departmentId)
    {
        var departmentToDelete = _departments.FirstOrDefault(d => d.DepartmentId == departmentId)
            ?? throw new ArgumentException("INVALID_DEPARTMENT_ID");

        _departments.Remove(departmentToDelete);
        _dbContext.Commit();
    }

    public IQueryable<Department> GetAll()
    {
        return _departments.AsQueryable();
    }

    public IQueryable<DepartmentStatistics> GetAllStatisticsForDepartment(int departmentId)
    {
        var department = _departments.FirstOrDefault(d => d.DepartmentId == departmentId)
            ?? throw new ArgumentException("INVALID_DEPARTMENT_ID");

        var statisticsForDepartment = _departmentStatistics
            .Where(ds => ds.DepartmentId == departmentId);

        return statisticsForDepartment;
    }

    public Department GetById(int departmentId)
    {
        var department = _departments.FirstOrDefault(d => d.DepartmentId == departmentId)
            ?? throw new ArgumentException("DEPARTMENT_NOT_FOUND");

        return department;
    }

    public void Update(Department updatedDepartment)
    {
        var department = _departments
            .FirstOrDefault(d => d.DepartmentId == updatedDepartment.DepartmentId)
                ?? throw new ArgumentException("INVALID_DEPARTMENT_ID");

        department = _mapper.Value.Map(updatedDepartment, department);

        _dbContext.Commit();
    }
}

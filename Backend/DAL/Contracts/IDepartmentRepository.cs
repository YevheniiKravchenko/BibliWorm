using Domain.Models;

namespace DAL.Contracts;
public interface IDepartmentRepository
{
    void Create(Department newDepartment);

    void Delete(int departmentId);

    IQueryable<Department> GetAll();

    Department GetById(int departmentId);

    void Update(Department updatedDepartment);

    void CreateDepartmentStatistics(DepartmentStatistics departmentStatistics);

    IQueryable<DepartmentStatistics> GetAllStatisticsForDepartment(int departmentId);
}

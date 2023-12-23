using BLL.Infrastructure.Models.Department;

namespace BLL.Contracts;
public interface IDepartmentService
{
    void AddDepartment(CreateUpdateDepartmentModel departmentModel);

    IEnumerable<DepartmentModel> GetAllDepartments();

    DepartmentModel GetDepartmentById(int departmentId);

    void DeleteDepartment(int departmentId);

    void UpdateDepartment(CreateUpdateDepartmentModel departmentModel);

    void AddStatisticsForDepartment(CreateDepartmentStatisticsModel departmentStatisticsModel);
}

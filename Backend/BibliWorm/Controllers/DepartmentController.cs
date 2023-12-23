using BLL.Contracts;
using BLL.Infrastructure.Models.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliWorm.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Librarian")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpPost("add")]
    public ActionResult AddDepartment([FromBody] CreateUpdateDepartmentModel departmentModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _departmentService.AddDepartment(departmentModel);

        return Ok();
    }

    [HttpPost("add-statistics")]
    [AllowAnonymous]
    public ActionResult AddStatisticsForDepartment([FromBody] CreateDepartmentStatisticsModel departmentStatisticsModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _departmentService.AddStatisticsForDepartment(departmentStatisticsModel);

        return Ok();
    }

    [HttpDelete("delete")]
    public ActionResult DeleteDepartment([FromQuery] int departmentId)
    {
        _departmentService.DeleteDepartment(departmentId);

        return Ok();
    }

    [HttpGet("get-all")]
    public ActionResult GetAllDepartments()
    {
        var departments = _departmentService.GetAllDepartments();

        return Ok(departments);
    }

    [HttpGet("get")]
    public ActionResult GetDepartmentById([FromQuery] int departmentId)
    {
        var department = _departmentService.GetDepartmentById(departmentId);

        return Ok(department);
    }

    [HttpPost("update")]
    public ActionResult UpdateDepartment([FromBody] CreateUpdateDepartmentModel departmentModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _departmentService.UpdateDepartment(departmentModel);

        return Ok();
    }
}

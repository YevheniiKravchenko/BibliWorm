using BibliWorm.Infrastructure.Models;
using BLL.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliWorm.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdministrationController : ControllerBase
{
    private readonly IAdministrationService _administrationService;

    public AdministrationController(IAdministrationService administrationService)
    {
        _administrationService = administrationService;
    }

    [HttpPost("backup-database")]
    public ActionResult BackupDatabase([FromBody] SavePathModel savePath)
    {
        _administrationService.BackupDatabase(savePath.SavePath);

        return Ok();
    }
}

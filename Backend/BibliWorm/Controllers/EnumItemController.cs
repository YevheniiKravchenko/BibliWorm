using BLL.Contracts;
using BLL.Infrastructure.Models.EnumItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliWorm.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Librarian")]
public class EnumItemController : ControllerBase
{
    private readonly IEnumItemService _enumItemService;

    public EnumItemController(IEnumItemService enumItemService)
    {
        _enumItemService = enumItemService;
    }

    [HttpGet("get-all")]
    public ActionResult GetAllEnumItems()
    {
        var enumItems = _enumItemService.GetAllEnumItems();

        return Ok(enumItems);
    }

    [HttpPost("add")]
    public ActionResult AddEnumItem([FromBody] EnumItemModel enumItemModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _enumItemService.AddEnumItem(enumItemModel);

        return Ok();
    }

    [HttpDelete("delete")]
    public ActionResult DeleteEnumItem([FromQuery] int enumItemId)
    {
        _enumItemService.DeleteEnumItem(enumItemId);

        return Ok();
    }

    [HttpGet("get")]
    public ActionResult GetEnumItemById([FromQuery] int enumItemId)
    {
        var enumItem = _enumItemService.GetEnumItemById(enumItemId);

        return Ok(enumItem);
    }

    [HttpPost("update")]
    public ActionResult UpdateEnumItem([FromBody] EnumItemModel enumItemModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _enumItemService.UpdateEnumItem(enumItemModel);

        return Ok();
    }
}

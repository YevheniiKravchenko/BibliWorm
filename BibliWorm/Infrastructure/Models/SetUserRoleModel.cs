using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace BibliWorm.Infrastructure.Models;
public class SetUserRoleModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public Role Role { get; set; }
}

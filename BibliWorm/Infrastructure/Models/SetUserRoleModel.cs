using Common.Enums;

namespace BibliWorm.Infrastructure.Models;
public class SetUserRoleModel
{
    public int UserId { get; set; }

    public Role Role { get; set; }
}

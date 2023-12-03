using Common.Enums;

namespace BLL.Infrastructure.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public Role Role { get; set; }
    }
}
using Common.Enums;

namespace BLL.Infrastructure.Models.User
{
    public class UserModel
    {
        public int UserId { get; set; }

        public Role Role { get; set; }
    }
}
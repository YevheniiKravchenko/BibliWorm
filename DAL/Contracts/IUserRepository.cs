using Common.Enums;
using DAL.Infrastructure.Models;
using DAL.Infrastructure.Models.Filters;
using Domain.Models;

namespace DAL.Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll(UserFilter filter);

        User GetUserById(int userId);

        void UpdateReaderCard(UserProfileInfo model);

        User LoginUser(string login, string password);

        void RegisterUser(RegisterUserModel model);

        void ResetPassword(string token, string newPassword);

        Guid CreateRefreshToken(int userId, int shiftInSeconds);

        User GetUserByRefreshToken(Guid refreshToken);

        User GetUserByEmail(string email);

        bool IsResetPasswordTokenValid(string token);

        string GenerateResetPasswordToken(int userId);

        void SetUserRole(int userId, Role role);
    }
}
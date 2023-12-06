using BLL.Infrastructure.Models;
using BLL.Infrastructure.Models.Book;
using BLL.Infrastructure.Models.EnumItem;
using Common.Enums;
using DAL.Infrastructure.Models;
using DAL.Infrastructure.Models.Filters;

namespace BLL.Contracts
{
    public interface IUserService
    {
        UserProfileModel GetUserProfileById(int userId);

        IEnumerable<UserProfileModel> GetAllUsers(UserFilter filter);

        void UpdateReaderCard(UserProfileInfo model);

        UserModel LoginUser(string login, string password);

        void RegisterUser(RegisterUserModel model);

        void ResetPassword(ResetPasswordModel resetPasswordModel);

        Guid CreateRefreshToken(int userId);

        UserModel GetUserByRefreshToken(Guid refreshToken);

        bool IsResetPasswordTokenValid(string token);

        void SetUserRole(int userId, Role role);

        void AddBookToFavourites(int userId, Guid bookId);

        void RemoveBookFromFavourites(int userId, Guid bookId);

        void ReserveBook(int userId, Guid bookId);

        void RemoveBookReserve(Guid reservationId);

        UserStatisticsModel GetUserStatistics(int userId);

        EnumItemModel GetUserFavouriteGenre(int userId);

        List<BookListItemModel> GetFavouriteBooks(int userId);
    }
}
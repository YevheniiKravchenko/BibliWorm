using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models;
using BLL.Infrastructure.Models.EnumItem;
using Common.Configs;
using Common.Enums;
using DAL.Contracts;
using DAL.Infrastructure.Models;
using Domain.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AuthOptions _authOptions;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IMapper> _mapper;

        public UserService(
            AuthOptions authOptions,
            Lazy<IUnitOfWork> unitOfWork,
            Lazy<IMapper> mapper)
        {
            _authOptions = authOptions;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Guid CreateRefreshToken(int userId)
        {
            return _unitOfWork.Value.Users.Value.CreateRefreshToken(
                userId,
                _authOptions.RefreshTokenLifetime
            );
        }

        public IEnumerable<UserProfileModel> GetAllUsers(PagingModel pagingModel)
        {
            var users = _unitOfWork.Value.Users.Value
                .GetAll(pagingModel)
                .ToList();

            var userProfiles = _mapper.Value.Map<List<UserProfileModel>>(users);

            return userProfiles;
        }

        public UserModel GetUserByRefreshToken(Guid refreshToken)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserByRefreshToken(refreshToken);
            var userModel = _mapper.Value.Map<UserModel>(user);

            return userModel;
        }

        public UserProfileModel GetUserProfileById(int userId)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserById(userId);
            var userProfileModel = _mapper.Value.Map<UserProfileModel>(user);

            return userProfileModel;
        }

        public UserModel LoginUser(string login, string password)
        {
            var user = _unitOfWork.Value.Users.Value.LoginUser(login, password);
            var userModel = _mapper.Value.Map<UserModel>(user);

            return userModel;
        }

        public void RegisterUser(RegisterUserModel model)
        {
            _unitOfWork.Value.Users.Value.RegisterUser(model);
        }

        public void ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            _unitOfWork.Value.Users.Value.ResetPassword(resetPasswordModel.Token, resetPasswordModel.Password);
        }

        public void UpdateReaderCard(UserProfileInfo model)
        {
            _unitOfWork.Value.Users.Value.UpdateReaderCard(model);
        }
        
        public bool IsResetPasswordTokenValid(string token)
        {
            return _unitOfWork.Value.Users.Value.IsResetPasswordTokenValid(token);
        }

        public void SetUserRole(int userId, Role role)
        {
            _unitOfWork.Value.Users.Value.SetUserRole(userId, role);
        }

        public void AddBookToFavourites(int userId, Guid bookId)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserById(userId);
            var book = _unitOfWork.Value.Books.Value.GetById(bookId);

            book.Users.Add(user);
            _unitOfWork.Value.Books.Value.Update(book);
        }

        public void RemoveBookFromFavourites(int userId, Guid bookId)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserById(userId);
            var book = _unitOfWork.Value.Books.Value.GetById(bookId);

            book.Users.Remove(user);
            _unitOfWork.Value.Books.Value.Update(book);
        }

        public void ReserveBook(int userId, Guid bookId)
        {
            var reservationQueue = new ReservationQueue
            {
                ReservationQueueId = Guid.Empty,
                ReservationDate = DateTime.UtcNow,
                BookId = bookId,
                UserId = userId
            };

            _unitOfWork.Value.ReservationQueues.Value.Create(reservationQueue);
        }

        public UserStatisticsModel GetUserStatistics(int userId)
        {
            var userStatistics = new UserStatisticsModel();
            var bookings = _unitOfWork.Value.Bookings.Value
                .GetAll()
                .Where(b => b.UserId == userId)
                .ToList();

            userStatistics.UserId = userId;
            userStatistics.TotalBooksRead = bookings
                .Where(b => b.ReturnedOnUtc != null)
                .Count();
            userStatistics.FavouriteGenre = GetUserFavouriteGenre(userId).Value;
            userStatistics.BiggestBookRead = bookings
                .Select(b => b.BookCopy.Book)
                .OrderByDescending(b => b.PagesAmount)
                .FirstOrDefault()?
                .Title ?? "";
            userStatistics.TotalPagesRead = bookings
                .Where(b => b.ReturnedOnUtc != null)
                .Select(b => b.BookCopy.Book)
                .Sum(b => b.PagesAmount);

            userStatistics.BookReadFastest = bookings
                .Where(b => b.ReturnedOnUtc != null)
                .OrderByDescending(b => b.ReturnedOnUtc - b.BookedOnUtc)
                .FirstOrDefault()?.BookCopy?.Book?.Title ?? "";

            userStatistics.CurrentlyReadingBooksAmount = bookings
                .Where(b => b.ReturnedOnUtc == null)
                .Count();

            userStatistics.WrittenReviewsAmount = _unitOfWork.Value.BookReviews.Value
                .GetAll()
                .Where(b => b.UserId == userId)
                .Count();

            return userStatistics;
        }

        public EnumItemModel GetUserFavouriteGenre(int userId)
        {
            var enumItem = _unitOfWork.Value.Bookings.Value.GetAll()
                .Where(b => b.UserId == userId)
                .SelectMany(b => b.BookCopy.Book.Genres)
                .GroupBy(g => g.EnumItemId)
                .Select(x => new
                {
                    EnumItemId = x.Key,
                    EnumItem = x.FirstOrDefault(y => y.EnumItemId == x.Key),
                    Amount = x.Where(y => y.EnumItemId == x.Key).Count()
                })
                .OrderByDescending(x => x.Amount)
                .FirstOrDefault()!.EnumItem;

            var enumItemModel = _mapper.Value.Map<EnumItemModel>(enumItem);

            return enumItemModel;
        }
    }
}

﻿using AutoMapper;
using Common.Enums;
using DAL.Contracts;
using DAL.DbContexts;
using DAL.Infrastructure.Extensions;
using DAL.Infrastructure.Helpers;
using DAL.Infrastructure.Models;
using DAL.Infrastructure.Models.Filters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextBase _dbContext;
        private readonly Lazy<IMapper> _mapper;

        private readonly DbSet<User> _users;
        private readonly DbSet<ReaderCard> _userProfiles;
        private readonly DbSet<RefreshToken> _refreshTokens;
        private readonly DbSet<ResetPasswordToken> _resetPasswordTokens;
        private readonly DbSet<Book> _books;
        private readonly DbSet<ReservationQueue> _reservationQueues;

        public UserRepository(
            DbContextBase dbContext,
            Lazy<IMapper> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _users = dbContext.Users;
            _userProfiles = dbContext.ReaderCards;
            _refreshTokens = dbContext.RefreshTokens;
            _resetPasswordTokens = dbContext.ResetPasswordTokens;
            _books = dbContext.Books;
            _reservationQueues = _dbContext.ReservationQueues;
        }

        public Guid CreateRefreshToken(int userId, int shiftInSeconds)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                _refreshTokens.RemoveRange(
                    _refreshTokens.Where(x => x.UserId == userId)
                );
                _dbContext.Commit();

                var refreshToken = new RefreshToken()
                {
                    UserId = userId,
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(shiftInSeconds)
                };
                _refreshTokens.Add(refreshToken);
                _dbContext.Commit();

                scope.Commit();

                return refreshToken.RefreshTokenId;
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<User> GetAll(UserFilter filter)
        {
            return filter.Filter(_users)
                .OrderBy(x => (x.ReaderCard.LastName + " " + x.ReaderCard.FirstName).Trim())
                .GetPage(filter.PagingModel);
        }

        public User GetUserById(int userId)
        {
            var user = _users
                .Include(u => u.ReaderCard)
                .Include(u => u.FavouriteBooks)
                .ThenInclude(fb => fb.Department)
                .FirstOrDefault(u => u.UserId == userId)
                    ?? throw new ArgumentException("INVALID_USERID");

            return user;
        }

        public User GetUserByRefreshToken(Guid refreshToken)
        {
            var token = _refreshTokens.FirstOrDefault(x =>
                x.RefreshTokenId == refreshToken
                && x.ExpiresOnUtc >= DateTime.UtcNow
            ) ?? throw new ArgumentException("INVALID_REFRESH_TOKEN");

            var user = _users.FirstOrDefault(x => x.UserId == token.UserId)
                ?? throw new ArgumentException("INVALID_USERID");

            return user;
        }

        public User LoginUser(string login, string password)
        {
            var user = _users.FirstOrDefault(x => x.Login == login
                || x.ReaderCard.Email == login);

            if (user is null
                || !HashHelper.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("INVALID_LOGIN_OR_PASSWORD");
            }

            return user;
        }

        public void RegisterUser(RegisterUserModel model)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                if (_users.Any(x => x.Login == model.Login))
                    throw new ArgumentException("LOGIN_EXISTS");

                if (_users.Any(x => x.ReaderCard.Email == model.Email))
                    throw new ArgumentException("EMAIL_EXISTS");

                var (salt, passwordHash) = HashHelper.GenerateNewPasswordHash(model.Password);
                var user = _mapper.Value.Map<User>(model);
                var readerCard = _mapper.Value.Map<ReaderCard>(model);
                if (readerCard.ProfilePicture == null)
                    readerCard.ProfilePicture = Array.Empty<byte>();

                user.PasswordSalt = salt;
                user.PasswordHash = passwordHash;
                user.RegistrationDate = DateTime.UtcNow;
                user.ReaderCard = readerCard;
                user.Role = Role.Member;

                _users.Add(user);
                _dbContext.Commit();

                scope.Commit();
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void ResetPassword(string token, string newPassword)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                var user = _users.Include(u => u.ResetPasswordTokens)
                    .FirstOrDefault(u => u.ResetPasswordTokens
                        .Any(t => t.Token == token
                            && t.ExpiresOnUtc >= DateTime.UtcNow))
                    ?? throw new ArgumentException("INVALID_RESET_PASSWORD_TOKEN");

                var (salt, passwordHash) = HashHelper.GenerateNewPasswordHash(newPassword);
                user.PasswordSalt = salt;
                user.PasswordHash = passwordHash;
                user.ResetPasswordTokens.Clear();

                _dbContext.Commit();

                scope.Commit();
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void UpdateReaderCard(UserProfileInfo model)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                var profile = _userProfiles.FirstOrDefault(p => p.UserId == model.UserId)
                ?? throw new ArgumentException("INVALID_USERID");

                _mapper.Value.Map(model, profile);
                _dbContext.Commit();

                scope.Commit();
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public User GetUserByEmail(string email)
        {
            var userProfile = _userProfiles
                .Include(p => p.User)
                .FirstOrDefault(p => p.Email == email);

            if (userProfile == null)
                throw new ArgumentException("USER_WITH_EMAIL_NOT_FOUND");

            return userProfile.User;
        }

        public bool IsResetPasswordTokenValid(string token)
        {
            var resetPasswordToken = _resetPasswordTokens.FirstOrDefault(x => x.Token == token
                && x.ExpiresOnUtc >= DateTime.UtcNow);

            return resetPasswordToken != null;
        }

        public string GenerateResetPasswordToken(int userId)
        {
            var scope = _dbContext.Database.BeginTransaction();

            try
            {
                _resetPasswordTokens.RemoveRange(
                    _resetPasswordTokens.Where(t => t.UserId == userId));
                _dbContext.Commit();

                var token = CreateRandomToken();
                var resetPasswordToken = new ResetPasswordToken()
                {
                    Token = token,
                    UserId = userId,
                    ExpiresOnUtc = DateTime.UtcNow.AddDays(1),
                };
                _resetPasswordTokens.Add(resetPasswordToken);

                _dbContext.Commit();
                
                scope.Commit();

                return token;
            }
            catch (Exception)
            {
                scope.Rollback();
                throw;
            }
        }

        public void SetUserRole(int userId, Role role)
        {
            var user = GetUserById(userId);

            user.Role = role;

            _dbContext.Commit();
        }

        private string CreateRandomToken()
        {
            var newToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            while(_resetPasswordTokens.Any(t => t.Token == newToken))
                newToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            return newToken;
        }
    }
}

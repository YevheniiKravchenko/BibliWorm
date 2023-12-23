using BibliWorm.Infrastructure.Models;
using BLL.Contracts;
using BLL.Infrastructure.Models.User;
using DAL.Infrastructure.Models;
using DAL.Infrastructure.Models.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IEmailService> _emailService;

        public UserController(
            Lazy<IUserService> userService,
            Lazy<IEmailService> emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.Value.RegisterUser(registerModel);

            return Ok();
        }

        [HttpPost("update")]
        public ActionResult UpdateReaderCard([FromBody] UserProfileInfo userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(userModel);

            _userService.Value.UpdateReaderCard(userModel);

            return Ok();
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword([FromBody] ForgotPasswordModel forgotPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _emailService.Value.SendResetPasswordEmail(forgotPassword.Email);

            return Ok();
        }

        [HttpGet("request-reset-password")]
        public ActionResult RequestResetPassword([FromQuery] string token)
        {
            var isTokenValid = _userService.Value.IsResetPasswordTokenValid(token);

            if (isTokenValid)
                return Ok();

            return BadRequest("Invalid reset password token");
        }

        [HttpPost("reset-password")]
        public ActionResult ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.Value.ResetPassword(resetPasswordModel);

            return Ok();
        }

        [HttpGet("get-all")]
        [Authorize]
        public ActionResult GetAll([FromQuery] UserFilter filter)
        {
            var users = _userService.Value.GetAllUsers(filter);
                
            return Ok(users);
        }

        [HttpGet("get")]
        [Authorize]
        public ActionResult GetUserProfileById([FromQuery] int userId)
        {
            var user = _userService.Value.GetUserProfileById(userId);

            return Ok(user);
        }

        [HttpPost("set-user-role")]
        [Authorize(Roles = "Admin")]
        public ActionResult SetUserRole([FromBody] SetUserRoleModel model)
        {
            _userService.Value.SetUserRole(model.UserId, model.Role);

            return Ok();
        }

        [HttpPost("add-book-to-favourites")]
        [Authorize]
        public ActionResult AddBookToFavourites([FromBody] AddRemoveBookFromFavouritesModel model)
        {
            _userService.Value.AddBookToFavourites(model.UserId, model.BookId);

            return Ok();
        }

        [HttpPost("remove-book-from-favourites")]
        [Authorize]
        public ActionResult RemoveBookFromFavourites([FromBody] AddRemoveBookFromFavouritesModel model)
        {
            _userService.Value.RemoveBookFromFavourites(model.UserId, model.BookId);

            return Ok();
        }

        [HttpPost("reserve-book")]
        [Authorize]
        public ActionResult ReserveBook([FromBody] ReserveBookModel model)
        {
            _userService.Value.ReserveBook(model.UserId, model.BookId);

            return Ok();
        }

        [HttpGet("get-favourite-books")]
        [Authorize]
        public ActionResult GetFavouriteBooks([FromQuery] int userId)
        {
            var favouriteBooks = _userService.Value.GetFavouriteBooks(userId);

            return Ok(favouriteBooks);
        }

        [HttpGet("get-user-statistics")]
        [Authorize]
        public ActionResult GetUserStatistics([FromQuery] int userId)
        {
            var userStatistics = _userService.Value.GetUserStatistics(userId);

            return Ok(userStatistics);
        }

        [HttpDelete("remove-book-reserve")]
        [Authorize]
        public ActionResult RemoveBookReserve([FromQuery] Guid reservationId)
        {
            _userService.Value.RemoveBookReserve(reservationId);

            return Ok();
        }
    }
}
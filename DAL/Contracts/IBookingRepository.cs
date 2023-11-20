using Domain.Models;

namespace DAL.Contracts;
public interface IBookingRepository
{
    void Create(Booking newBooking);

    void Delete(Guid bookingId);

    IQueryable<Booking> GetAll();

    Booking GetById(Guid bookingId);

    void Update(Booking updatedBooking);
}

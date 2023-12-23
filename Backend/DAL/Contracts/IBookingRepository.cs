using Domain.Models;

namespace DAL.Contracts;
public interface IBookingRepository
{
    void Create(Booking newBooking);

    void Create(List<Booking> newBookings);

    void Delete(Guid bookingId);

    IQueryable<Booking> GetAll();

    Booking GetById(Guid bookingId);

    void Update(Booking updatedBooking);

    void Update(List<Booking> updatedBookings);
}

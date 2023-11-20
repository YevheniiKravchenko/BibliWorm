using AutoMapper;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contracts;
public class BookingRepository : IBookingRepository
{
    private readonly DbContextBase _dbContext;
    private readonly Lazy<IMapper> _mapper;

    private readonly DbSet<Booking> _bookings;
    private readonly DbSet<BookCopy> _bookCopies;

    public BookingRepository(
        DbContextBase dbContext,
        Lazy<IMapper> mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _bookings = dbContext.Bookings;
        _bookCopies = dbContext.BookCopies;
    }

    public void Create(Booking newBooking)
    {
        var book = _bookCopies.FirstOrDefault(bc => bc.BookCopyId == newBooking.BookCopyId)
            ?? throw new ArgumentException("INVALID_BOOK_COPY_ID");

        _bookings.Add(newBooking);
        _dbContext.Commit();
    }

    public void Delete(Guid bookingId)
    {
        var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId)
            ?? throw new ArgumentException("INVALID_BOOKING_ID");

        _bookings.Remove(booking);
        _dbContext.Commit();
    }

    public IQueryable<Booking> GetAll()
    {
        return _bookings.AsQueryable();
    }

    public Booking GetById(Guid bookingId)
    {
        var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId)
            ?? throw new ArgumentException("BOOKING_NOT_FOUND");

        return booking;
    }

    public void Update(Booking updatedBooking)
    {
        var booking = _bookings.FirstOrDefault(b => b.BookingId == updatedBooking.BookingId)
            ?? throw new ArgumentException("INVALID_BOOKING_ID");

        booking = _mapper.Value.Map(updatedBooking, booking);
        _dbContext.Commit();
    }
}

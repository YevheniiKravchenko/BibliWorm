using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class ReservationQueueRepository : IReservationQueueRepository
{
    private readonly DbContextBase _dbContext;
    private readonly Lazy<IMapper> _mapper;

    private readonly DbSet<ReservationQueue> _reservationQueues;
    private readonly DbSet<Book> _books;

    public ReservationQueueRepository(
        DbContextBase dbContext,
        Lazy<IMapper> mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _reservationQueues = dbContext.ReservationQueues;
        _books = dbContext.Books;
    }

    public void Create(ReservationQueue newReservationQueue)
    {
        var book = _books.FirstOrDefault(b => b.BookId == newReservationQueue.BookId)
            ?? throw new ArgumentException("INVALID_BOOK_ID");

        _reservationQueues.Add(newReservationQueue);
        _dbContext.Commit();
    }

    public void Delete(Guid reservationQueueId)
    {
        var reservationQueueToDelete = _reservationQueues
            .FirstOrDefault(rq => rq.ReservationQueueId == reservationQueueId)
                ?? throw new ArgumentException("INVALID_RESERVATION_QUEUE_ID");

        _reservationQueues.Remove(reservationQueueToDelete);
        _dbContext.Commit();
    }

    public void Delete(List<Guid> reservationQueuesIds) 
    {
        var reservationQueuesToDelete = GetAll()
            .Where(rq => reservationQueuesIds.Contains(rq.ReservationQueueId))
            .ToList();

        _reservationQueues.RemoveRange(reservationQueuesToDelete);
        _dbContext.Commit();
    }

    public IQueryable<ReservationQueue> GetAll()
    {
        return _reservationQueues.AsQueryable();
    }

    public ReservationQueue GetById(Guid reservationQueueId)
    {
        var reservationQueue = _reservationQueues
            .FirstOrDefault(rq => rq.ReservationQueueId == reservationQueueId)
                ?? throw new ArgumentException("RESERVATION_QUEUE_NOT_FOUND");

        return reservationQueue;
    }

    public void Update(ReservationQueue updatedReservationQueue)
    {
        var reservationQueue = _reservationQueues
            .FirstOrDefault(rq => rq.ReservationQueueId == updatedReservationQueue.ReservationQueueId)
                ?? throw new ArgumentException("INVALID_RESERVATION_QUEUE_ID");

        reservationQueue = _mapper.Value.Map(updatedReservationQueue, reservationQueue);
        _dbContext.Commit();
    }
}

using Domain.Models;

namespace DAL.Contracts;
public interface IReservationQueueRepository
{
    void Create(ReservationQueue newReservationQueue);

    void Delete(Guid reservationQueueId);

    void Delete(List<Guid> reservationQueuesIds);

    IQueryable<ReservationQueue> GetAll();

    ReservationQueue GetById(Guid reservationQueueId);

    void Update(ReservationQueue updatedReservationQueue);
}

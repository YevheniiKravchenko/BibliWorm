namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        Lazy<IUserRepository> Users { get; }

        Lazy<IBookRepository> Books { get; }

        Lazy<IBookCopyRepository> BookCopies { get; }

        Lazy<IBookingRepository> Bookings { get; }

        Lazy<IBookReviewRepository> BookReviews { get; }

        Lazy<IDepartmentRepository> Departments { get; }

        Lazy<IEnumItemRepository> EnumItems { get; }

        Lazy<IReservationQueueRepository> ReservationQueues { get; }
    }
}

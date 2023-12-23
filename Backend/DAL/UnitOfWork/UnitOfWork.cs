using DAL.Contracts;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            Lazy<IUserRepository> users,
            Lazy<IBookRepository> books,
            Lazy<IBookCopyRepository> bookCopies,
            Lazy<IBookingRepository> bookings,
            Lazy<IBookReviewRepository> bookReviews,
            Lazy<IDepartmentRepository> departments,
            Lazy<IEnumItemRepository> enumItems,
            Lazy<IReservationQueueRepository> reservationQueues,
            Lazy<IAdministrationRepository> administration)
        {
            Users = users;
            Books = books;
            BookCopies = bookCopies;
            Bookings = bookings;
            BookReviews = bookReviews;
            Departments = departments;
            EnumItems = enumItems;
            ReservationQueues = reservationQueues;
            Administration = administration;
        }

        public Lazy<IUserRepository> Users { get; }

        public Lazy<IBookRepository> Books { get; }

        public Lazy<IBookCopyRepository> BookCopies { get; }

        public Lazy<IBookingRepository> Bookings { get; }

        public Lazy<IBookReviewRepository> BookReviews { get; }

        public Lazy<IDepartmentRepository> Departments { get; }

        public Lazy<IEnumItemRepository> EnumItems { get; }

        public Lazy<IReservationQueueRepository> ReservationQueues { get; }

        public Lazy<IAdministrationRepository> Administration { get; }
    }
}
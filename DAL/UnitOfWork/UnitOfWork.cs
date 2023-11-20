using DAL.Contracts;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            Lazy<IUserRepository> users,
            Lazy<IBookRepository> books)
        {
            Users = users;
            Books = books;
        }

        public Lazy<IUserRepository> Users { get; }

        public Lazy<IBookRepository> Books { get; set; }
    }
}
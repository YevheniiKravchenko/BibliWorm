namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        Lazy<IUserRepository> Users { get; }

        Lazy<IBookRepository> Books { get; }
    }
}

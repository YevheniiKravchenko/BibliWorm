using DAL.Infrastructure.Models.Filters;
using Domain.Models;

namespace DAL.Contracts;
public interface IBookRepository
{
    IQueryable<Book> GetAll();

    Book GetById(Guid bookId);

    IQueryable<Book> GetAll(BookFilter filter);

    void Create(Book newBook);

    void Update(Book updatedBook);

    void Delete(Guid bookId);
}

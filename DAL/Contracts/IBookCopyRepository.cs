using Domain.Models;

namespace DAL.Contracts;
public interface IBookCopyRepository
{
    void Create(BookCopy newBookCopy);

    void Delete(Guid bookCopyId);

    IQueryable<BookCopy> GetAll();

    BookCopy GetById(Guid bookCopyId);

    void Update(BookCopy updatedBookCopy);
}

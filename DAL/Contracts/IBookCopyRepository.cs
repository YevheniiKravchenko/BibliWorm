using Domain.Models;

namespace DAL.Contracts;
public interface IBookCopyRepository
{
    void Create(BookCopy newBookCopy);

    void Delete(Guid bookCopyId);

    IQueryable<BookCopy> GetAll();

    BookCopy GetById(Guid bookCopyId);

    BookCopy GetByRFID(string rfid);

    void Update(BookCopy updatedBookCopy);

    void SetBookCopiesAsUnavailable(List<Guid> bookCopiesIds);
}

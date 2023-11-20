using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class BookCopyRepository : IBookCopyRepository
{
    private readonly DbContextBase _dbContext;
    private readonly Lazy<IMapper> _mapper;

    private readonly DbSet<Book> _books;
    private readonly DbSet<BookCopy> _bookCopies;

    public BookCopyRepository(
        DbContextBase dbContext,
        Lazy<IMapper> mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _books = dbContext.Books;
        _bookCopies = dbContext.BookCopies;
    }

    public void Create(BookCopy newBookCopy)
    {
        var book = _books.FirstOrDefault(b => b.BookId == newBookCopy.BookId)
            ?? throw new ArgumentException("INVALID_BOOK_ID");

        _bookCopies.Add(newBookCopy);
        _dbContext.Commit();
    }

    public void Delete(Guid bookCopyId)
    {
        var bookCopyToDelete = _bookCopies.FirstOrDefault(bc => bc.BookCopyId == bookCopyId)
            ?? throw new ArgumentException("INVALID_BOOK_COPY_ID");

        _bookCopies.Remove(bookCopyToDelete);
        _dbContext.Commit();
    }

    public IQueryable<BookCopy> GetAll()
    {
        return _bookCopies.AsQueryable();
    }

    public BookCopy GetById(Guid bookCopyId)
    {
        var bookCopy = _bookCopies.FirstOrDefault(bc => bc.BookCopyId == bookCopyId)
            ?? throw new ArgumentException("BOOK_COPY_NOT_FOUND");

        return bookCopy;
    }

    public void Update(BookCopy updatedBookCopy)
    {
        var bookCopy = _bookCopies.FirstOrDefault(bc => bc.BookCopyId == updatedBookCopy.BookCopyId)
            ?? throw new ArgumentException("INVALID_BOOK_COPY_ID");

        bookCopy = _mapper.Value.Map(updatedBookCopy, bookCopy);

        _dbContext.Commit();
    }
}

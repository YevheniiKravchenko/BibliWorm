using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using DAL.Infrastructure.Extensions;
using DAL.Infrastructure.Models.Filters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class BookRepository : IBookRepository
{
    private readonly Lazy<IMapper> _mapper;
    private readonly DbContextBase _dbContext;
    private readonly DbSet<Book> _books;
    private readonly DbSet<Department> _departments;

    public BookRepository(
        DbContextBase dbContext,
        Lazy<IMapper> mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;

        _books = dbContext.Books;
        _departments = dbContext.Departments;
    }
    public void Create(Book newBook)
    {
        if (newBook.DepartmentId != null)
        {
            var department = _departments.FirstOrDefault(d => d.DepartmentId == newBook.DepartmentId.Value)
                ?? throw new ArgumentException("INVALID_DEPARTMENT_ID");
        }

        _books.Add(newBook);
        _dbContext.Commit();
    }

    public void Delete(Guid bookId)
    {
        var bookToDelete = _books.FirstOrDefault(b => b.BookId == bookId)
            ?? throw new ArgumentException("INVALID_BOOK_ID");

        _books.Remove(bookToDelete);
        _dbContext.Commit();
    }

    public IQueryable<Book> GetAll()
    {
        return _books.AsQueryable();
    }

    public IQueryable<Book> GetAll(BookFilter filter)
    {
        var books = filter.Filter(_books)
            .OrderBy(x => x.Title)
            .GetPage(filter.PagingModel);

        return books;
    }

    public Book GetById(Guid bookId)
    {
        var book = _books.FirstOrDefault(b => b.BookId == bookId)
            ?? throw new ArgumentException("BOOK_NOT_FOUND");

        return book;
    }

    public void Update(Book updatedBook)
    {
        var book = _books.FirstOrDefault(b => b.BookId == updatedBook.BookId)
            ?? throw new ArgumentException("INVALID_BOOK_ID");

        if (updatedBook.DepartmentId != null
            && book.DepartmentId != updatedBook.DepartmentId)
        {
            var department = _departments
                .FirstOrDefault(d => d.DepartmentId == updatedBook.DepartmentId.Value)
                    ?? throw new ArgumentException("INVALID_DEPARTMENT_ID");
        }

        book = _mapper.Value.Map(updatedBook, book);

        _dbContext.Commit();
    }
}

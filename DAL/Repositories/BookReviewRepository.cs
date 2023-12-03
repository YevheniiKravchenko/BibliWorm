﻿using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class BookReviewRepository : IBookReviewRepository
{
    private readonly DbContextBase _dbContext;
    private readonly Lazy<IMapper> _mapper;
    
    private readonly DbSet<BookReview> _bookReviews;

    public BookReviewRepository(DbContextBase dbContext, Lazy<IMapper> mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _bookReviews = dbContext.BookReviews;
    }

    public void Create(BookReview newBookReview)
    {
        _bookReviews.Add(newBookReview);
        _dbContext.Commit();
    }

    public void Delete(Guid bookId)
    {
        var bookReviewToDelete = _bookReviews.FirstOrDefault(br => br.BookReviewId == bookId)
            ?? throw new ArgumentException("INVALID_BOOK_REVIEW_ID");

        _bookReviews.Remove(bookReviewToDelete);
        _dbContext.Commit();
    }

    public IQueryable<BookReview> GetAll()
    {
        return _bookReviews.AsQueryable()
            .Include(br => br.User)
            .ThenInclude(u => u.ReaderCard);
    }

    public BookReview GetById(Guid reviewId)
    {
        var bookReview = _bookReviews
            .Include(br => br.User)
            .ThenInclude(u => u.ReaderCard)
            .FirstOrDefault(br => br.BookReviewId == reviewId)
                ?? throw new ArgumentException("BOOK_REVIEW_NOT_FOUND");

        return bookReview;
    }

    public void Update(BookReview updatedBookReview)
    {
        var bookReview = _bookReviews.FirstOrDefault(br => br.BookReviewId == updatedBookReview.BookReviewId)
            ?? throw new ArgumentException("INVALID_BOOK_REVIEW_ID");

        bookReview = _mapper.Value.Map(updatedBookReview, bookReview);

        _dbContext.Commit();
    }
}

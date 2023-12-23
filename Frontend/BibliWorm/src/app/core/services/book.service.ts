import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUpdateBookModel } from '../models/book/create-update-book-model';
import { api, extractData } from '../constants/api-constants';
import { Observable } from 'rxjs';
import { CreateUpdateBookCopyModel } from '../models/book/create-update-book-copy-model';
import { BookListItemModel } from '../models/book/book-list-item-model';
import { BookModel } from '../models/book/book-model';
import { BookCopyListItemModel } from '../models/book/book-copy-list-item-model';
import { BookCopyModel } from '../models/book/book-copy-model';
import { BookingListItemModel } from '../models/book/booking-list-item-model';

@Injectable({
    providedIn: 'root'
})
export class BookService {

    constructor(
        private http: HttpClient
    ) { }

    public addBook(bookModel: CreateUpdateBookModel): Observable<any> {
        var url = api.book.addBook;

        return this.http.post(
            url,
            bookModel
        ).pipe(extractData());
    }

    public addBookCopy(bookCopyModel: CreateUpdateBookCopyModel): Observable<any> {
        var url = api.book.addBookCopy;

        return this.http.post(
            url,
            bookCopyModel
        ).pipe(extractData());
    }

    public bookList(titleSearch: string): Observable<BookListItemModel[]> {
        var url = api.book.bookList;
        var params = new HttpParams().set('title', titleSearch);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<BookListItemModel[]>());
    }

    public deleteBook(bookId: string): Observable<any> {
        var url = api.book.deleteBook;
        var params = new HttpParams().set('bookId', bookId);

        return this.http.delete(
            url,
            { params: params }
        ).pipe(extractData());
    }

    public deleteBookCopy(bookCopyId: string): Observable<any> {
        var url = api.book.deleteBookCopy;
        var params = new HttpParams().set('bookCopyId', bookCopyId);

        return this.http.delete(
            url,
            { params: params }
        ).pipe(extractData());
    }

    public getBookById(bookId: string): Observable<BookModel> {
        var url = api.book.getBookById;
        var params = new HttpParams().set('bookId', bookId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<BookModel>());
    }

    public getBookCopies(bookId: string): Observable<BookCopyListItemModel[]> {
        var url = api.book.getBookCopies;
        var params = new HttpParams().set('bookId', bookId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<BookCopyListItemModel[]>());
    }

    public getBookCopyById(bookCopyId: string): Observable<BookCopyModel> {
        var url = api.book.getBookCopyById;
        var params = new HttpParams().set('bookCopyId', bookCopyId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<BookCopyModel>());
    }

    public getMostPopularBooks(): Observable<BookListItemModel[]> {
        var url = api.book.getMostPopularBooks;

        return this.http.get(url)
            .pipe(extractData<BookListItemModel[]>());
    }

    public getMostPopularBooksInGenre(genreId: number): Observable<BookListItemModel[]> {
        var url = api.book.getMostPopularBooksInGenre;
        var params = new HttpParams().set('genreId', genreId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<BookListItemModel>());
    }

    public updateBook(bookModel: CreateUpdateBookModel): Observable<any> {
        var url = api.book.updateBook;

        return this.http.post(
            url,
            bookModel
        ).pipe(extractData());
    }

    public updateBookCopy(bookCopyModel: CreateUpdateBookCopyModel): Observable<any> {
        var url = api.book.updateBookCopy;

        return this.http.post(
            url,
            bookCopyModel
        ).pipe(extractData());
    }

    public getUserBookings(userId: number): Observable<BookingListItemModel[]> {
        var url = api.book.getUserBookings;
        var params = new HttpParams().set('userId', userId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<BookingListItemModel>());
    }

    public returnBookCopies(bookingsIds: string[]): Observable<any> {
        var url = api.book.returnBookCopies;

        return this.http.post(
            url,
            bookingsIds
        ).pipe(extractData());
    }
}

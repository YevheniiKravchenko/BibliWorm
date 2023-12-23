import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { BookListItemModel } from '../../core/models/book/book-list-item-model';
import { BookService } from '../../core/services/book.service';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EnumItemService } from '../../core/services/enum-item.service';
import { EnumItemModel } from '../../core/models/enum-item/enum-item-model';
import { NIL as NIL_UUID } from 'uuid';
import { BookEditComponent } from '../book-edit/book-edit.component';
import { Router } from '@angular/router';
import { setActiveButton } from '../../core/helpers/helpers';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrl: './books.component.scss'
})
export class BooksComponent implements OnInit {
    
    searchQuery: string = '';
    selectedGenreId: number = 1;
    emptyGuid: string = NIL_UUID;
    books$: Observable<BookListItemModel[]>;
    genres$: Observable<EnumItemModel[]>;
    
    constructor(
        private bookService: BookService,
        private enumItemService: EnumItemService,
        private toastr: ToastrService,
        private modalService: NgbModal,
        private router: Router,
    ) { }

    ngOnInit(): void {
        this.updateBooks(this.searchQuery);
        this.getGenres();
    }

    ngAfterViewInit(): void {
        setActiveButton('#booksBtn');
    }

    updateBooks(searchQuery: string): void {
        this.bookService.bookList(searchQuery).subscribe({
            next: (books) => {
                this.books$ = of(books);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    onSearchClick(): void {
        this.updateBooks(this.searchQuery);
    }

    onBooksButtonClick(bookId: string): void {
        this.router.navigate(['/book-copies', bookId]);
    }

    onEditButtonClick(bookId: string): void {
        var modalRef = this.modalService.open(BookEditComponent);
        modalRef.componentInstance.bookId = bookId;

        modalRef.result.then((result) => {
            if (result) {
                this.updateBooks(this.searchQuery);
            }
        }).catch(() => { });
    }

    onDeleteButtonClick(bookId: string): void {
        var deleteResult$ = this.bookService
            .deleteBook(bookId);

        deleteResult$.subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation success');
                this.updateBooks(this.searchQuery);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    onGetMostPopularBooksButtonClick(): void {
        this.bookService.getMostPopularBooks().subscribe({
            next: (books) => {
                this.books$ = of(books);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }
    
    onGetMostPopularBooksInGenreButtonClick(): void {
        this.bookService.getMostPopularBooksInGenre(this.selectedGenreId).subscribe({
            next: (books) => {
                this.books$ = of(books);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    getGenres(): void {
        this.enumItemService.getAllEnumItems().subscribe({
            next: (genres) => {
                this.genres$ = of(genres);
                
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    identify(index: number, item: any){
        return item.name;
    }
}

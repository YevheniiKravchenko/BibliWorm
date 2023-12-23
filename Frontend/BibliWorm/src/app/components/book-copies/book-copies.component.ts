import { Component, OnInit } from '@angular/core';
import { BookService } from '../../core/services/book.service';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NIL as NIL_UUID } from 'uuid';
import { Observable, of } from 'rxjs';
import { BookCopyListItemModel } from '../../core/models/book/book-copy-list-item-model';
import { ActivatedRoute } from '@angular/router';
import { BookCopyEditComponent } from '../book-copy-edit/book-copy-edit.component';

@Component({
  selector: 'app-book-copies',
  templateUrl: './book-copies.component.html',
  styleUrl: './book-copies.component.scss'
})
export class BookCopiesComponent implements OnInit {

    bookId: string;
    emptyGuid: string = NIL_UUID;
    bookCopies$: Observable<BookCopyListItemModel[]>;

    constructor(
        private bookService: BookService,
        private toastr: ToastrService,
        private modalService: NgbModal,
        private route: ActivatedRoute,
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.bookId = params['bookId'];

            if (this.bookId) {
                this.updateBookCopies();
            }
        });
    }

    updateBookCopies(): void {
        this.bookService.getBookCopies(this.bookId).subscribe({
            next: (bookCopies) => {
                this.bookCopies$ = of(bookCopies);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    onEditButtonClick(bookCopyId: string): void {
        var modalRef = this.modalService.open(BookCopyEditComponent);
        modalRef.componentInstance.bookCopyId = bookCopyId;
        modalRef.componentInstance.bookId = this.bookId;
        
        modalRef.result.then((result) => {
            if (result) {
                this.updateBookCopies();
            }
        }).catch(() => { });
    }

    onDeleteButtonClick(bookCopyId: string): void {
        var deleteResult$ = this.bookService
            .deleteBookCopy(bookCopyId);

        deleteResult$.subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation success');
                this.updateBookCopies();
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }
}

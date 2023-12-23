import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BookService } from '../../core/services/book.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { BookModel } from '../../core/models/book/book-model';
import { KeyValue, formatDate } from '@angular/common';
import { NIL as NIL_UUID } from 'uuid';

@Component({
    selector: 'app-book-edit',
    templateUrl: './book-edit.component.html',
    styleUrl: './book-edit.component.scss'
})
export class BookEditComponent implements OnInit {

    activeModal = inject(NgbActiveModal);
    bookForm!: FormGroup;
    departments$: Observable<KeyValue<number, string>[]>;
    genres$: Observable<KeyValue<number, string>[]>;
    book: BookModel;

    @Input() bookId: string;

    constructor(
        private formBuilder: FormBuilder,
        private bookService: BookService,
        private toastr: ToastrService,
    ) { }

    ngOnInit(): void {
        this.initForm();
        this.fillBookForm();
    }

    onSubmit(): void {
        var genresControl = this.bookForm.get('genres');
        if (genresControl?.touched) {
            var newGenreValue = genresControl.value;
            this.bookForm.patchValue({
                genres: [newGenreValue]
            });
        }

        var operationResult$: Observable<any>;
        if (this.bookId !== NIL_UUID) {
            operationResult$ = this.bookService
                .updateBook(this.bookForm.value);
        } else {
            operationResult$ = this.bookService
                .addBook(this.bookForm.value);
        }

        operationResult$.subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation successful');
                this.activeModal.close(true);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');  
            }
        });
    }

    identify(index: number, item: any){
        return item.name;
    }

    private initForm(): void {
        this.bookForm = this.formBuilder.group({
            'bookId': [this.bookId, Validators.required],
            'title': ['', [Validators.required, Validators.minLength(1), Validators.maxLength(200)]],
            'author': ['', Validators.maxLength(252)],
            'isbn': ['', Validators.required],
            'publicationDate': [''],
            'publisher': ['', Validators.maxLength(252)],
            'description': ['', Validators.maxLength(512)],
            'pagesAmount': ['', [Validators.required, Validators.min(1)]],
            'coverImage': [''],
            'keyWords': ['', Validators.maxLength(512)],
            'departmentId': [''],
            'genres': []
        });
    }

    private fillBookForm(): void {
        var book$ = this.getBook();
        book$.subscribe({
            next: (book) => {
                this.bookForm.patchValue({
                    title: book.title,
                    author: book.author,
                    isbn: book.isbn,
                    publicationDate: formatDate(book.publicationDate, 'yyyy-MM-dd', 'en'),
                    publisher: book.publisher,
                    description: book.description,
                    pagesAmount: book.pagesAmount,
                    keyWords: book.keyWords,
                    departmentId: book.departmentId,
                    genres: book.bookGenres
                });

                this.book = book;
                this.departments$ = of(book.departments);
                this.genres$ = of(book.genres);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    private getBook(): Observable<BookModel> {
        return this.bookService.getBookById(this.bookId);
    }

}

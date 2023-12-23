import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BookService } from '../../core/services/book.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { BookCopyModel } from '../../core/models/book/book-copy-model';
import { NIL as NIL_UUID } from 'uuid';

@Component({
  selector: 'app-book-copy-edit',
  templateUrl: './book-copy-edit.component.html',
  styleUrl: './book-copy-edit.component.scss'
})
export class BookCopyEditComponent implements OnInit {

    activeModal = inject(NgbActiveModal);
    bookCopyForm!: FormGroup;

    @Input() bookCopyId: string;
    @Input() bookId: string;

    constructor(
        private formBuilder: FormBuilder,
        private bookService: BookService,
        private toastr: ToastrService,
    ) { }

    ngOnInit(): void {
        this.initForm();
        if (this.bookCopyId !== NIL_UUID) {
            this.fillBookCopyForm();
        }
    }

    onSubmit(): void {
        var operationResult$: Observable<any>;
        if (this.bookCopyId !== NIL_UUID) {
            operationResult$ = this.bookService
                .updateBookCopy(this.bookCopyForm.value);
        } else {
            operationResult$ = this.bookService
                .addBookCopy(this.bookCopyForm.value);
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

    private initForm(): void {
        this.bookCopyForm = this.formBuilder.group({
            'bookCopyId': [this.bookCopyId, Validators.required],
            'rfid': ['', Validators.required],
            'condition': ['', Validators.maxLength(252)],
            'isAvailable': [true],
            'bookId': [this.bookId, Validators.required]
        });
    }

    private fillBookCopyForm(): void {  
        var bookCopy$ = this.getBookCopy();
        bookCopy$.subscribe({
            next: (bookCopy) => {
                this.bookCopyForm.patchValue({
                    rfid: bookCopy.rfid,
                    condition: bookCopy.condition,
                    isAvailable: bookCopy.isAvailable,
                });
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    private getBookCopy(): Observable<BookCopyModel> {
        return this.bookService.getBookCopyById(this.bookCopyId);
    }

}

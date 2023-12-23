import { Component, OnInit } from '@angular/core';
import { BookService } from '../../core/services/book.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { BookingListItemModel } from '../../core/models/book/booking-list-item-model';
import { Observable, of } from 'rxjs';

@Component({
    selector: 'app-user-bookings',
    templateUrl: './user-bookings.component.html',
    styleUrl: './user-bookings.component.scss'
})
export class UserBookingsComponent implements OnInit {

    userId: number;
    userBookings$: Observable<BookingListItemModel[]>;

    constructor(
        private bookService: BookService,
        private toastr: ToastrService,
        private route: ActivatedRoute,
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.userId = params['userId'];

            if (this.userId) {
                this.updateUserBookings();
            }
        });
    }

    updateUserBookings(): void {
        this.bookService.getUserBookings(this.userId).subscribe({
            next: (userBookings) => {
                this.userBookings$ = of(userBookings);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    onReturnButtonClick(bookingId: string): void {
        var bookingsIds = [bookingId];
        var returnResult$ = this.bookService.returnBookCopies(bookingsIds);

        returnResult$.subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation success');
                this.updateUserBookings();
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }
}

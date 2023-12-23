import { Component, OnInit } from '@angular/core';
import { EnumItemService } from '../../core/services/enum-item.service';
import { EnumItemModel } from '../../core/models/enum-item/enum-item-model';
import { Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GenreEditComponent } from '../genre-edit/genre-edit.component';
import { setActiveButton } from '../../core/helpers/helpers';

@Component({
    selector: 'app-genres',
    templateUrl: './genres.component.html',
    styleUrl: './genres.component.scss'
})
export class GenresComponent implements OnInit {

    genres$: Observable<EnumItemModel[]>;

    constructor(
        private enumItemService: EnumItemService,
        private toastr: ToastrService,
        private modalService: NgbModal,
    ) { }

    ngOnInit(): void {
        this.updateGenres()
    }

    ngAfterViewInit(): void {
        setActiveButton('#genresBtn');
    }

    updateGenres(): void {
        this.enumItemService.getAllEnumItems().subscribe({
            next: (enumItems) => {
                this.genres$ = of(enumItems);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    onEditButtonClick(enumItemId: number): void {
        var modalRef = this.modalService.open(GenreEditComponent);
        modalRef.componentInstance.genreId = enumItemId;

        modalRef.result.then((result) => {
            if (result) {
                this.updateGenres();
            }
        }).catch(() => { });
    }

    onDeleteButtonClick(enumItemId: number): void {
        var deleteResult$ = this.enumItemService
        .deleteEnumItem(enumItemId);

        deleteResult$.subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation success');
                this.updateGenres();
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

}

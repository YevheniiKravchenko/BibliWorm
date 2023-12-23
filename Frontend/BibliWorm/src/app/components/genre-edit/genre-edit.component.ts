import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnumItemService } from '../../core/services/enum-item.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { EnumItemModel } from '../../core/models/enum-item/enum-item-model';

@Component({
    selector: 'app-genre-edit',
    templateUrl: './genre-edit.component.html',
    styleUrl: './genre-edit.component.scss'
})
export class GenreEditComponent implements OnInit {

    enumId: number = 1;
    activeModal = inject(NgbActiveModal);
    genreForm!: FormGroup;

    @Input() genreId: number;

    constructor(
        private formBuilder: FormBuilder,
        private enumItemService: EnumItemService,
        private toastr: ToastrService,
    ) { }

    ngOnInit(): void {
        this.initForm();
        if (this.genreId > 0) {
            this.fillGenreForm();
        }
    }

    onSubmit(): void {
        var operationResult$: Observable<any>;
        if (this.genreId > 0) {
            operationResult$ = this.enumItemService
                .updateEnumItem(this.genreForm.value);
        } else {
            operationResult$ = this.enumItemService
                .addEnumItem(this.genreForm.value);
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
        this.genreForm = this.formBuilder.group({
            'enumItemId': [this.genreId, Validators.required],
            'value': ['', [Validators.required, Validators.maxLength(64)]],
            'enumId': [this.enumId, Validators.required],
        });
    }

    private fillGenreForm(): void {
        var genre$ = this.getGenre();
        genre$.subscribe({
            next: (genre) => {
                this.genreForm.patchValue({
                    value: genre.value,
                    enumId: genre.enumId
                });
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    private getGenre(): Observable<EnumItemModel> {
        return this.enumItemService.getEnumItemById(this.genreId);
    }

}

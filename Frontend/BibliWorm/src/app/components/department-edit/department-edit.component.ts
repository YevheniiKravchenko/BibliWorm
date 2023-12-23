import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DepartmentService } from '../../core/services/department.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { DepartmentModel } from '../../core/models/department/department-model';

@Component({
    selector: 'app-department-edit',
    templateUrl: './department-edit.component.html',
    styleUrl: './department-edit.component.scss'
})
export class DepartmentEditComponent implements OnInit {

    activeModal = inject(NgbActiveModal);
    departmentForm!: FormGroup

    @Input() departmentId: number;

    constructor(
        private formBuilder: FormBuilder,
        private departmentService: DepartmentService,
        private toastr: ToastrService,
    ) { }

    ngOnInit(): void {
        this.initForm();
        if (this.departmentId > 0) {
            this.fillDepartmentForm();
        }
    }

    onSubmit(): void {
        var operationResult$: Observable<any>;
        if (this.departmentId > 0) {
            operationResult$ = this.departmentService
                .updateDepartment(this.departmentForm.value);
        } else {
            operationResult$ = this.departmentService
                .addDepartment(this.departmentForm.value);
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
        this.departmentForm = this.formBuilder.group({
            'departmentId': [this.departmentId, Validators.required],
            'name': ['', [Validators.required, Validators.maxLength(32)]],
        });
    }

    private fillDepartmentForm(): void {
        var department$ = this.getDepartment();
        department$.subscribe({
            next: (department) => {
                this.departmentForm.patchValue({
                    name: department.name
                });
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    private getDepartment(): Observable<DepartmentModel> {
        return this.departmentService.getDepartmentById(this.departmentId);
    }
    
}

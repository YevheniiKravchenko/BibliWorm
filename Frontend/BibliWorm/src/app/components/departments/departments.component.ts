import { Component, OnInit } from '@angular/core';
import { DepartmentService } from '../../core/services/department.service';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { DepartmentModel } from '../../core/models/department/department-model';
import { DepartmentEditComponent } from '../department-edit/department-edit.component';
import { setActiveButton } from '../../core/helpers/helpers';

@Component({
    selector: 'app-departments',
    templateUrl: './departments.component.html',
    styleUrl: './departments.component.scss'
})
export class DepartmentsComponent implements OnInit {

    departments$: Observable<DepartmentModel[]>;

    constructor(
        private departmentService: DepartmentService,
        private toastr: ToastrService,
        private modalService: NgbModal,
    ) { }

    ngOnInit(): void {
        this.updateDepartments();
    }

    ngAfterViewInit(): void {
        setActiveButton('#departmentsBtn');
    }

    updateDepartments(): void {
        this.departmentService.getAllDepartments().subscribe({
            next: (departments) => {
                this.departments$ = of(departments);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    onEditButtonClick(departmentId: number): void {
        var modalRef = this.modalService.open(DepartmentEditComponent);
        modalRef.componentInstance.departmentId = departmentId;

        modalRef.result.then((result) => {
            if (result) {
                this.updateDepartments();
            }
        }).catch(() => { });
    }

    onDeleteButtonClick(departmentId: number): void {
        var deleteResult$ = this.departmentService
            .deleteDepartment(departmentId);

        deleteResult$.subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation success');
                this.updateDepartments();
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

}

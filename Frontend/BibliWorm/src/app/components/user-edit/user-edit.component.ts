import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../core/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { UserProfileModel } from '../../core/models/user/user-profile-model';
import { formatDate } from '@angular/common' 

@Component({
    selector: 'app-user-edit',
    templateUrl: './user-edit.component.html',
    styleUrl: './user-edit.component.scss'
})
export class UserEditComponent implements OnInit {

    activeModal = inject(NgbActiveModal);
    userForm!: FormGroup;

    @Input() userId: number;

    constructor(
        private formBuilder: FormBuilder,
        private userService: UserService,
        private toastr: ToastrService,
    ) { }

    ngOnInit(): void {
        this.initForm();
        this.fillUserForm();
    }

    onSubmit(): void {
        this.userService.updateReaderCard(this.userForm.value).subscribe({
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
        this.userForm = this.formBuilder.group({
            'userId': [this.userId, Validators.required],
            'firstName': ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
            'lastName': ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
            'address': ['', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
            'phoneNumber': ['', Validators.required],
            'birthDate': ['', Validators.required],
            'email': ['', [Validators.required, Validators.email]],
        });
    }

    private fillUserForm(): void {
        var user$ = this.getUser();
        user$.subscribe({
            next: (user) => {
                this.userForm.patchValue({
                    firstName: user.firstName,
                    lastName: user.lastName,
                    address: user.address,
                    phoneNumber: user.phoneNumber,
                    birthDate: formatDate(user.birthDate, 'yyyy-MM-dd', 'en'),
                    email: user.email,
                });
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    private getUser(): Observable<UserProfileModel> {
        return this.userService.getUserProfileById(this.userId);
    }
}

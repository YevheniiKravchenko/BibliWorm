import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { StorageService } from '../../core/services/storage.service';
import { ToastrService } from 'ngx-toastr';
import { setActiveButton } from '../../core/helpers/helpers';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

    loginForm!: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private storageService: StorageService,
        private router: Router,
        private toastr: ToastrService
    ) { }

    ngOnInit(): void {
        this.loginForm = this.formBuilder.group({
            'login': new FormControl('', Validators.required),
            'password': new FormControl('', Validators.required),
        });

        if (this.storageService.isLoggedIn()) {
            this.router.navigate(['/books']);
        }
    }

    ngAfterViewInit(): void {
        setActiveButton('#loginBtn');
    }

    onSubmit(): void {
        this.authService.login(this.loginForm.value).subscribe({
            next: (token) => {
                this.storageService.saveToken(token);
                this.toastr.success('Operation successfull', 'Success',);
                this.router.navigate(['/books']);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }
}

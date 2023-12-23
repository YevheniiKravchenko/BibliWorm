import { Component, OnInit } from '@angular/core';
import { UserService } from '../../core/services/user.service';
import { Observable, of } from 'rxjs';
import { UserProfileModel } from '../../core/models/user/user-profile-model';
import { ToastrService } from 'ngx-toastr';
import { StorageService } from '../../core/services/storage.service';
import { Role } from '../../core/enums/role';
import { SetUserRoleModel } from '../../core/models/user/set-user-role-model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserEditComponent } from '../user-edit/user-edit.component';
import { Router } from '@angular/router';
import { setActiveButton } from '../../core/helpers/helpers';

@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {

    searchQuery: string = '';
    users$: Observable<UserProfileModel[]>

    constructor(
        private userService: UserService,
        private toastr: ToastrService,
        private storageService: StorageService,
        private modalService: NgbModal,
        private router: Router
    ) { }
    
    ngOnInit(): void {
        this.updateUsers(this.searchQuery);
    }
    
    ngAfterViewInit(): void {
        setActiveButton('#usersBtn');
    }

    updateUsers(searchQuery: string): void {
        this.userService.getAll(searchQuery).subscribe({
            next: (users) => {
                this.users$ = of(users);
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    isAdmin(): boolean {
        return this.storageService.isAdmin();
    }

    getRoles(): any[] {
        var options = [];

        for (var key in Role) {
            if (isNaN(Number(key))) {
                options.push({
                    value: Role[key],
                    label: key
                });
            }
        }

        return options;
    }

    onSearchClick(): void {
        var searchInput = <HTMLInputElement>document.querySelector("input#search");
        var searchQuery = searchInput.value;

        this.updateUsers(searchQuery);
    }

    onChange(event: Event, userId: number) {
        var newRole = +(<HTMLSelectElement>event.currentTarget).value;
        var setUserRoleModel: SetUserRoleModel = {
            userId: userId,
            role: newRole,
        };

        this.userService.setUserRole(setUserRoleModel).subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation success');
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');
            }
        });
    }

    identify(index: number, item: any) {
        return item.name;
    }

    onBookButtonClick(userId: number) {
        this.router.navigate(['user-bookings', userId]);
    }

    onCardButtonClick(userId: number): void {
        var modalRef = this.modalService.open(UserEditComponent);
        modalRef.componentInstance.userId = userId;

        modalRef.result.then((result) => {
            if (result) {
                this.updateUsers(this.searchQuery);
            }
        }).catch(() => { });
    }
}

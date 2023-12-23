import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { GenresComponent } from './components/genres/genres.component';
import { BooksComponent } from './components/books/books.component';
import { BookCopiesComponent } from './components/book-copies/book-copies.component';
import { UserBookingsComponent } from './components/user-bookings/user-bookings.component';
import { AdministrationComponent } from './components/administration/administration.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full',
    },
    {
        path: 'registration',
        component: RegistrationComponent,
    },
    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path: 'users',
        component: UsersComponent,
    },
    {
        path: 'departments',
        component: DepartmentsComponent,
    },
    {
        path: 'genres',
        component: GenresComponent,
    },
    {
        path: 'books',
        component: BooksComponent
    },
    {
        path: 'book-copies/:bookId',
        component: BookCopiesComponent,
    },
    {
        path: 'user-bookings/:userId',
        component: UserBookingsComponent
    },
    {
        path: 'administration',
        component: AdministrationComponent,
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
